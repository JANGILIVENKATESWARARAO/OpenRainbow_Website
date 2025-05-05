using static ORS_website.Server.Models.ViewModels.WebsiteViewModel;

namespace ORS_website.Server.Contracts
{
    public interface IDbRepository
    {
        Task<T> ExecuteProcedureV2Async<T>(string storedProcedureName, object? parameters = null, string? connectionString = null, int? timeout = null);
    }
}
