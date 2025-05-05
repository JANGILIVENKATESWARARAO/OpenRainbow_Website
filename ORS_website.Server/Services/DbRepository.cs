using Microsoft.Data.SqlClient;
using ORS_website.Server.Contracts;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace ORS_website.Server.Services
{
    public sealed class DbRepository : IDbRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public DbRepository(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }
        public async Task<T> ExecuteProcedureV2Async<T>(string storedProcedureName, object? parameters = null, string? connectionString = null, int? timeout = null)
        {
            Type targetType = typeof(T);

            connectionString = ResolveConnectionString(connectionString);

            using SqlConnection conn = new(connectionString);
            SqlCommand cmd = BuildSqlCommand(storedProcedureName, conn, parameters, timeout);

            await conn.OpenAsync();

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            IList results;

            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Namespace!.StartsWith("System.Collections"))
            {
                Type listElementType = targetType.GetGenericArguments()[0];
                results = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listElementType))!;
            }
            else
            {
                results = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(targetType))!;
            }

            while (await dr.ReadAsync())
            {
                Type listElementType = results.GetType().GetGenericArguments()[0];
                if (listElementType.Namespace == "System")
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        if (dr[0] != DBNull.Value)
                        {
                            Type underlyingType = Nullable.GetUnderlyingType(listElementType) ?? listElementType;
                            results.Add(Convert.ChangeType(dr[i], underlyingType));
                        }
                        else
                        {
                            if (listElementType == typeof(string))
                            {
                                results.Add(null);
                            }
                            else
                            {
                                object listElement = Activator.CreateInstance(listElementType);

                                results.Add(listElement);
                            }
                        }
                    }
                }
                else
                {
                    this.MapDataReaderToObjectV2(dr, results);
                }
            }
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Namespace!.StartsWith("System.Collections"))
            {
                return (T)results;
            }

            return results.Count > 0 ? (T)results[0] : default;
        }

        private string? ResolveConnectionString(string? connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = this.httpContextAccessor.HttpContext?.Items["ConnectionString"]?.ToString();

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    connectionString = this.configuration.GetConnectionString("DbConnection");
                }
            }

            return connectionString;
        }
        private SqlCommand BuildSqlCommand(string storedProcedureName, SqlConnection conn, object? parameters, int? timeout = null)
        {
            SqlCommand cmd = new(storedProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                Type type = parameters.GetType();

                if (type.IsGenericType && type.Name.Contains("AnonymousType") && type.Name.StartsWith("<>") && type.Attributes.HasFlag(TypeAttributes.NotPublic))
                {
                    foreach (var prop in parameters.GetType().GetProperties())
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            if (!string.IsNullOrWhiteSpace((string?)prop.GetValue(parameters)))
                            {
                                cmd.Parameters.AddWithValue((prop.Name.StartsWith("p_") ? "@" : "@p_") + prop.Name, prop.GetValue(parameters));
                            }
                        }
                        else if (prop.GetValue(parameters) != null)
                        {
                            cmd.Parameters.AddWithValue((prop.Name.StartsWith("p_") ? "@" : "@p_") + prop.Name, prop.GetValue(parameters));
                        }
                    }
                }
                else
                {
                    this.SetParameters(type, cmd, parameters);
                }
            }

            if (timeout != null && timeout > 0)
            {
                cmd.CommandTimeout = (int)timeout;
            }

            return cmd;
        }
        private void SetParameters(Type type, SqlCommand cmd, object parameters)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.PropertyType.Namespace == type.Namespace)
                {
                    this.SetParameters(propertyInfo.PropertyType, cmd, propertyInfo.GetValue(parameters));
                }

                string? columnName = propertyInfo.GetCustomAttribute<ColumnAttribute>()?.Name;

                if (!string.IsNullOrEmpty(columnName))
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        if (!string.IsNullOrWhiteSpace((string?)propertyInfo.GetValue(parameters)))
                        {
                            cmd.Parameters.AddWithValue($"@p_{columnName}", propertyInfo.GetValue(parameters));
                        }
                    }
                    else if (propertyInfo.GetValue(parameters) != null)
                    {
                        cmd.Parameters.AddWithValue($"@p_{columnName}", propertyInfo.GetValue(parameters));
                    }
                }
            }
        }
        private void MapDataReaderToObjectV2(SqlDataReader dr, IList results)
        {
            var targetType = results.GetType().GetGenericArguments()[0];
            object obj = Activator.CreateInstance(targetType);

            foreach (PropertyInfo propertyInfo in targetType.GetProperties())
            {
                if (propertyInfo.PropertyType.Namespace == targetType.Namespace)
                {
                    IList nestedResults = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(propertyInfo.PropertyType))!;

                    MapDataReaderToObjectV2(dr, nestedResults);
                    propertyInfo.SetValue(obj, nestedResults[0]);
                }
                else if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Namespace.StartsWith("System.Collections"))
                {
                    Type nestedListObj = propertyInfo.PropertyType.GetGenericArguments()[0];
                    IList nestedResults = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(nestedListObj))!;

                    MapDataReaderToObjectV2(dr, nestedResults);

                    var existingList = (IList)propertyInfo.GetValue(obj);
                    if (existingList == null)
                    {
                        propertyInfo.SetValue(obj, nestedResults);
                    }
                    else
                    {
                        foreach (var nestedResult in nestedResults)
                        {
                            existingList.Add(nestedResult);
                        }

                        propertyInfo.SetValue(obj, existingList);
                    }
                }

                string columnName = propertyInfo.GetCustomAttribute<ColumnAttribute>()?.Name ?? propertyInfo.Name;
                List<string> names = columnName.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => n.Trim())
                    .ToList();

                string? name = names.FirstOrDefault(x => HasColumn(dr, x));

                if (!string.IsNullOrEmpty(name))
                {
                    object value = dr[name];
                    if (value != DBNull.Value)
                    {
                        Type underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                        propertyInfo.SetValue(obj, Convert.ChangeType(value, underlyingType));
                    }
                }
            }

            if (targetType.GetProperties().Any(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition().Namespace.StartsWith("System.Collections")))
            {
                bool matchFound = false;
                foreach (object existingObject in results)
                {
                    bool allPropertiesMatch = true;

                    foreach (PropertyInfo propertyInfo in existingObject.GetType().GetProperties())
                    {
                        if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Namespace.StartsWith("System.Collections"))
                        {
                            continue;
                        }

                        string existingValue = propertyInfo.GetValue(existingObject)?.ToString();
                        string newValue = propertyInfo.GetValue(obj)?.ToString();

                        if (existingValue != newValue)
                        {
                            allPropertiesMatch = false;
                            break;
                        }
                    }

                    if (allPropertiesMatch)
                    {
                        matchFound = true;

                        foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
                        {
                            if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Namespace.StartsWith("System.Collections"))
                            {
                                var existingList = (IList)propertyInfo.GetValue(existingObject);
                                var nestedResults = (IList)propertyInfo.GetValue(obj);

                                foreach (var nestedResult in nestedResults)
                                {
                                    existingList.Add(nestedResult);
                                }
                            }
                        }
                        break;
                    }
                }

                if (matchFound)
                {
                    return;
                }
            }

            results.Add(obj);
        }
        private bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
