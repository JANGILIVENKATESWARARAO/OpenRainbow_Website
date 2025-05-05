namespace ORS_website.Server.Models.Common
{
    public class DBScripts
    {
    --#region Experience
        CREATE TABLE Tbl_Experience(
            E_Id INT IDENTITY(1,1) PRIMARY KEY,
            Experience VARCHAR(20) NOT NULL 
        );

        
        CREATE PROCEDURE S_InsExperience 
        @P_Experience VARCHAR(20) 
        AS 
        BEGIN 
            IF @P_Experience IS NULL OR LTRIM(RTRIM(@P_Experience)) = ''  
            BEGIN 
                RAISERROR('Experience cannot be NULL or whitespace.', 16, 1); 
                RETURN; 
            END; 

            INSERT INTO Tbl_Experience (Experience) 
            VALUES (LTRIM(RTRIM(@P_Experience))); 
            Go;
            EXEC S_GetExperience  
        END;

        CREATE PROCEDURE S_GetExperience 
        AS 
        BEGIN 
            SELECT E_Id, Experience FROM Tbl_Experience (NOLOCK); 
        END; 

        --#region Skills

        --#region Questionary  

        --#region Category

        --#region AdminCareer

        --#region ApllyCareer
    }
}
