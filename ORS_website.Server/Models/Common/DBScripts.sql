-- Create Table Query for Experience 

CREATE TABLE Tbl_Experience (
    E_Id INT IDENTITY(1,1) PRIMARY KEY,
    Experience VARCHAR(20) NOT NULL
);

-- Insert Procedure for Experience 

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
    -- Call to another stored procedure, if needed
    EXEC S_GetExperience;
END;

-- Fetch Procedure for Experience 

CREATE PROCEDURE S_GetExperience
AS
BEGIN
    SELECT E_Id, Experience FROM Tbl_Experience (NOLOCK);
END;

-- Create Table script for Skills 

CREATE TABLE Tbl_Skills (
    S_Id INT IDENTITY(1,1) PRIMARY KEY,
    SkillName VARCHAR(20) NOT NULL
);

-- Insert query for Skills

Insert into Tbl_Skills (SkillName) Values
('HTML', 'CSS', 'BootStrap', 'JavaScript', 'Angular', 'React', 'RxJS', 'C#', 'LINQ', 'WebAPI', '.NET Core', 
'MVC', 'EF Core', 'JWT', 'OAuth', 'SQL Server', 'Oracle', 'MongoDB', 'Micro Services', 'GIT', 'Git Hub',
'Gitlab', 'AWS', 'Docker', 'Azure', 'Kubernetes', 'CI/CD', 'Agile', 'Posman', 'SOAP UI', 'Swagger', 'VS Code',
'Visual Studio', 'SSMS','Scrum')


-- Insert Procedure for Skills 

CREATE PROCEDURE S_InsSkills
    @P_SkillName VARCHAR(20)
AS
BEGIN
    -- Check if the parameter is NULL or exactly one space
    IF @P_SkillName IS NULL OR LTRIM(RTRIM(@P_SkillName)) = '' 
    BEGIN
        RAISERROR('Skill cannot be NULL or a white space.', 16, 1);
        RETURN;
    END;

    -- Insert if valid
    INSERT INTO Tbl_Skills (SkillName)
    VALUES (LTRIM(RTRIM(@P_SkillName)));
END;

-- Fetch Procedure for Skills 

CREATE PROCEDURE S_GetSkills
AS
BEGIN
    SELECT S_Id, SkillName FROM Tbl_Skills (NOLOCK);
END;

-- Create Table Query for Skills 

CREATE TABLE Tbl_Skills (
    S_Id INT IDENTITY(1,1) PRIMARY KEY,
    SkillName VARCHAR(20) NOT NULL
);

-- Insert Procedure for Skills 

CREATE PROCEDURE S_InsSkills
    @P_SkillName VARCHAR(20)
AS
BEGIN
    -- Check if the parameter is NULL or exactly one space
    IF @P_SkillName IS NULL OR LTRIM(RTRIM(@P_SkillName)) = '' 
    BEGIN
        RAISERROR('Skill cannot be NULL or a white space.', 16, 1);
        RETURN;
    END;
    -- Insert if valid
    INSERT INTO Tbl_Skills (SkillName)
    VALUES (LTRIM(RTRIM(@P_SkillName)));
END;

-- Fetch Procedure for Skills 

CREATE PROCEDURE S_GetSkills
AS
BEGIN
    SELECT S_Id, SkillName FROM Tbl_Skills (NOLOCK);
END;

-- Create Table Query for Category 

CREATE TABLE Tbl_Category (
    C_Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryType VARCHAR(20) NOT NULL
);

-- Insert Procedure for Category 

CREATE PROCEDURE S_InsCategory
    @P_CategoryType VARCHAR(20)
AS
BEGIN
    -- Check if the parameter is NULL or exactly one space
    IF @P_CategoryType IS NULL OR LTRIM(RTRIM(@P_CategoryType)) = ''
    BEGIN
        RAISERROR('CategoryType cannot be NULL or a white space.', 16, 1);
        RETURN;
    END;

    -- Insert if valid
    INSERT INTO Tbl_Category (CategoryType)
    VALUES (@P_CategoryType);
    EXEC S_GetCategory
END;

-- Fecth Procedure for Category Table 

CREATE PROCEDURE S_GetCategory
AS
BEGIN
    SELECT C_Id, CategoryType FROM Tbl_Category (NOLOCK);
END;


-- Create Table query for AdminCareer Table

CREATE TABLE Tbl_AdminCareer (
    Id INT IDENTITY(1,1),  -- Hidden auto-incrementing number
    Job_Id AS ('ORJO' + RIGHT('0000' + CAST(Id AS VARCHAR), 4)) PERSISTED PRIMARY KEY,
    JobTitle VARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Experience_Id INT NOT NULL,
    Skill_Id INT NOT NULL,
    NoOfOpenings INT NOT NULL,
    StartDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    EndDate DATE NOT NULL,
    CONSTRAINT FK_AdminCareer_Experience FOREIGN KEY (Experience_Id)
        REFERENCES Tbl_Experience(E_Id),
    CONSTRAINT FK_AdminCareer_Skills FOREIGN KEY (Skill_Id)
        REFERENCES Tbl_Skills(S_Id)
);

-- Insert Procedure for AdminCareer 

CREATE PROCEDURE S_InsAdminCareer
    @P_JobTitle VARCHAR(50),
    @P_Description NVARCHAR(MAX),
    @P_Experience_Id INT,
    @P_Skill_Id INT,
    @P_NoOfOpenings INT,
    @P_EndDate DATE
AS
BEGIN
    -- Declare a variable to store the current date for comparison and default usage
    DECLARE @CurrentDate DATE = CAST(GETDATE() AS DATE);

    -- Validate that JobTitle is not NULL or empty
    IF NULLIF(LTRIM(RTRIM(@P_JobTitle)), '') IS NULL
    BEGIN
        RAISERROR('JobTitle cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    -- Validate that Description is not NULL or empty
    IF NULLIF(LTRIM(RTRIM(@P_Description)), '') IS NULL
    BEGIN
        RAISERROR('Description cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    -- Validate that Experience_Id is valid and exists in Tbl_Experience
    IF NOT EXISTS (SELECT 1 FROM Tbl_Experience WHERE E_Id = @P_Experience_Id)
    BEGIN
        RAISERROR('Invalid Experience_Id. The Experience_Id must exist in Tbl_Experience.', 16, 1);
        RETURN;
    END;

    -- Validate that Skill_Id is valid and exists in Tbl_Skills
    IF NOT EXISTS (SELECT 1 FROM Tbl_Skills WHERE Skill_Id = @P_Skill_Id)
    BEGIN
        RAISERROR('Invalid Skill_Id. The Skill_Id must exist in Tbl_Skills.', 16, 1);
        RETURN;
    END;

    -- Validate that NoOfOpenings is a positive number
    IF @P_NoOfOpenings <= 0
    BEGIN
        RAISERROR('NoOfOpenings must be greater than 0.', 16, 1);
        RETURN;
    END;

    -- Validate that EndDate is a valid future date and greater than or equal to StartDate
    IF @P_EndDate < @CurrentDate
    BEGIN
        RAISERROR('EndDate must be a future date.', 16, 1);
        RETURN;
    END;

    -- Insert the data into Tbl_AdminCareer
    INSERT INTO Tbl_AdminCareer (JobTitle, Description, Experience_Id, Skill_Id, NoOfOpenings, StartDate, EndDate)
    VALUES (
        @P_JobTitle, 
        @P_Description, 
        @P_Experience_Id, 
        @P_Skill_Id, 
        @P_NoOfOpenings, 
        @CurrentDate,  -- Use the current date for StartDate
        @P_EndDate
    );
    EXEC S_GetCareers
END;

-- Fetch Procedure for AdminCareer Table

CREATE PROCEDURE S_GetCareers
AS
BEGIN
    -- Select required columns using INNER JOINs
    SELECT 
        ac.Job_Id,
        ac.JobTitle,
        ac.Description,
        e.Experience,  -- Experience from Tbl_Experience
        s.SkillName AS Skill,           -- Skill from Tbl_Skills
        ac.NoOfOpenings,
        ac.EndDate
    FROM 
        Tbl_AdminCareer ac
    INNER JOIN 
        Tbl_Experience e ON ac.Experience_Id = e.E_Id    -- INNER JOIN with Tbl_Experience on Experience_Id
    INNER JOIN 
        Tbl_Skills s ON ac.Skill_Id = s.S_Id         -- INNER JOIN with Tbl_Skills on Skill_Id
END;

-- Create Table Query for Questinary

CREATE TABLE Tbl_Questionary (
    Q_Id INT IDENTITY(1,1) PRIMARY KEY,
    PhoneNo VARCHAR(15) NOT NULL,
    Mail VARCHAR(50) NOT NULL,
    Questions NVARCHAR(2000) NOT NULL
);

-- Insert Procedure for Questionary

CREATE PROCEDURE S_InsQuestionary
    @P_PhoneNo VARCHAR(15),
    @P_Mail VARCHAR(50),
    @P_Questions NVARCHAR(2000)
AS
BEGIN
    -- Validation: Check for required fields
    IF NULLIF(LTRIM(RTRIM(@P_PhoneNo)), '') IS NULL
    BEGIN
        RAISERROR('PhoneNo cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    IF NULLIF(LTRIM(RTRIM(@P_Mail)), '') IS NULL
    BEGIN
        RAISERROR('Mail cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    -- Insert into Tbl_Questionary
    INSERT INTO Tbl_Questionary (PhoneNo, Mail, Questions)
    VALUES (@P_PhoneNo, @P_Mail, @P_Questions);
    EXEC S_GetQuestionary
END;

-- Fetch Procedure for Questionary 
CREATE PROCEDURE S_GetQuestionary
AS
BEGIN
    SELECT 
        Q_Id,
        PhoneNo,
        Mail,
        Questions
    FROM 
        Tbl_Questionary (NOLOCK)
END;

-- Create Table Query For Apply Career 

CREATE TABLE Tbl_ApplyCareer (
    AC_Id INT IDENTITY(1,1) PRIMARY KEY,
    Job_Id VARCHAR(15) NOT NULL,
    Name VARCHAR(50) NOT NULL,
    PhoneNo VARCHAR(15) NOT NULL,
    PrevOrg VARCHAR(50) NOT NULL,
    PrevOrgLocation VARCHAR(200),
    CurrentCTC VARCHAR(10),
    ExpectedCTC VARCHAR(10),
    Reason VARCHAR(2000) NOT NULL,
    Resume VARBINARY(5242880) NOT NULL,         -- Up to 5 MB
    CoverLetter VARBINARY(5242880),             -- Optional, up to 5 MB
    TermsAndConditions BIT NOT NULL,

    -- Foreign Key Constraint
    CONSTRAINT FK_ApplyCareer_AdminCareer FOREIGN KEY (Job_Id)
        REFERENCES Tbl_AdminCareer(Job_Id)
);

-- Insert Procedure for ApplyCareer

CREATE PROCEDURE S_InsApplyCareer
    @P_Job_Id VARCHAR(15),
    @P_Name VARCHAR(50),
    @P_PhoneNo VARCHAR(15),
    @P_PrevOrg VARCHAR(50),
    @P_PrevOrgLocation VARCHAR(200) = NULL,
    @P_CurrentCTC VARCHAR(10) = NULL,
    @P_ExpectedCTC VARCHAR(10) = NULL,
    @P_Reason VARCHAR(2000),
    @P_Resume VARBINARY(MAX),
    @P_CoverLetter VARBINARY(MAX) = NULL,
    @P_TermsAndConditions BIT
AS
BEGIN
    -- Validate required text fields
    IF NULLIF(LTRIM(RTRIM(@P_Name)), '') IS NULL
    BEGIN
        RAISERROR('Name cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    IF NULLIF(LTRIM(RTRIM(@P_PhoneNo)), '') IS NULL
    BEGIN
        RAISERROR('PhoneNo cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    IF NULLIF(LTRIM(RTRIM(@P_PrevOrg)), '') IS NULL
    BEGIN
        RAISERROR('Previous Organization cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    IF NULLIF(LTRIM(RTRIM(@P_Reason)), '') IS NULL
    BEGIN
        RAISERROR('Reason cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    -- Validate Job_Id exists in Tbl_AdminCareer
    IF NOT EXISTS (SELECT 1 FROM Tbl_AdminCareer WHERE Job_Id = @P_Job_Id)
    BEGIN
        RAISERROR('Invalid Job_Id. It must exist in Tbl_AdminCareer.', 16, 1);
        RETURN;
    END;

    -- Validate resume is provided and not larger than 5MB
    IF @P_Resume IS NULL OR DATALENGTH(@P_Resume) > 5242880
    BEGIN
        RAISERROR('Resume is required and must be less than or equal to 5 MB.', 16, 1);
        RETURN;
    END;

    -- Optional: Validate CoverLetter size
    IF @P_CoverLetter IS NOT NULL AND DATALENGTH(@P_CoverLetter) > 5242880
    BEGIN
        RAISERROR('CoverLetter must be less than or equal to 5 MB.', 16, 1);
        RETURN;
    END;

    -- Validate Terms and Conditions
    IF @P_TermsAndConditions IS NULL OR @P_TermsAndConditions <> 1
    BEGIN
        RAISERROR('Terms and Conditions must be accepted.', 16, 1);
        RETURN;
    END;

    -- Insert into Tbl_ApplyCareer
    INSERT INTO Tbl_ApplyCareer (
        Job_Id, Name, PhoneNo, PrevOrg, PrevOrgLocation,
        CurrentCTC, ExpectedCTC, Reason, Resume, CoverLetter, TermsAndConditions
    )
    VALUES (
        @P_Job_Id, @P_Name, @P_PhoneNo, @P_PrevOrg, @P_PrevOrgLocation,
        @P_CurrentCTC, @P_ExpectedCTC, @P_Reason, @P_Resume, @P_CoverLetter, @P_TermsAndConditions
    );
    EXEC S_GetApplyCareer
END;

-- Fetch Procedure for Apply Career 

CREATE PROCEDURE S_GetApplyCareer
AS
BEGIN
    SELECT
        jc.JobTitle,
        ac.Name,
        ac.PhoneNo,
        ac.PrevOrg,
        ac.PrevOrgLocation,
        ac.CurrentCTC,
        ac.ExpectedCTC,
        ac.Reason,
        ac.Resume,
        ac.CoverLetter,
        ac.TermsAndConditions,
        jc.EndDate
    FROM 
        Tbl_ApplyCareer ac
    INNER JOIN 
        Tbl_AdminCareer jc ON ac.Job_Id = jc.Job_Id
    ORDER BY 
        ac.AC_Id;
END;

-- Create Table Query for AdminBlog

CREATE TABLE Tbl_AdminBlog (
    Blog_Id INT IDENTITY(1,1) PRIMARY KEY,          -- Auto-incrementing primary key
    BlogTitle VARCHAR(50) NOT NULL,                  -- Title of the blog (required)
    BlogSubTitle VARCHAR(50),                        -- Subtitle of the blog (optional)
    PostedBy VARCHAR(30) NOT NULL,                   -- Name of the person posting the blog (required)
    PostedDate DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),  -- Date only (current date)
    Description NVARCHAR(MAX) NOT NULL,              -- Description of the blog (required)
    Category_Id INT,                                 -- Foreign key to Tbl_Category
    ImageOrVideo VARBINARY(MAX),                     -- Binary data for storing image or video content
    Hyperlink VARCHAR(200),                          -- Optional hyperlink related to the blog
EndDate DATE NOT NULL
    -- Foreign Key Constraint
    CONSTRAINT FK_AdminBlog_Category FOREIGN KEY (Category_Id)
        REFERENCES Tbl_Category(C_Id)
);

-- Insert Procedure for AdminBlog

CREATE PROCEDURE S_InsAdminBlog
    @P_BlogTitle VARCHAR(50),
    @P_BlogSubTitle VARCHAR(50) = NULL,
    @P_PostedBy VARCHAR(30),
    @P_Description NVARCHAR(MAX),
    @P_Category_Id INT,
    @P_ImageOrVideo VARBINARY(MAX) = NULL,
    @P_Hyperlink VARCHAR(200) = NULL,
    @P_EndDate DATE 
AS
BEGIN
    -- Validate required fields
    IF NULLIF(LTRIM(RTRIM(@P_BlogTitle)), '') IS NULL
    BEGIN
        RAISERROR('BlogTitle cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    IF NULLIF(LTRIM(RTRIM(@P_PostedBy)), '') IS NULL
    BEGIN
        RAISERROR('PostedBy cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    IF NULLIF(LTRIM(RTRIM(@P_Description)), '') IS NULL
    BEGIN
        RAISERROR('Description cannot be NULL or empty.', 16, 1);
        RETURN;
    END;

    -- Validate Category_Id exists in Tbl_Category
    IF NOT EXISTS (SELECT 1 FROM Tbl_Category WHERE Category_Id = @P_Category_Id)
    BEGIN
        RAISERROR('Invalid Category_Id. It must exist in Tbl_Category.', 16, 1);
        RETURN;
    END;

    -- Validate that EndDate is greater than PostedDate (current date)
    IF @P_EndDate < CAST(GETDATE() AS DATE)
    BEGIN
        RAISERROR('EndDate must be greater than the PostedDate (current date).', 16, 1);
        RETURN;
    END

    -- Insert the data into Tbl_AdminBlog
    INSERT INTO Tbl_AdminBlog (
        BlogTitle, BlogSubTitle, PostedBy, PostedDate, Description, Category_Id, ImageOrVideo, Hyperlink, EndDate
    )
    VALUES (
        @P_BlogTitle, @P_BlogSubTitle, @P_PostedBy, CAST(GETDATE() AS DATE), @P_Description, @P_Category_Id, @P_ImageOrVideo, @P_Hyperlink, @P_EndDate
    );
    EXEC S_GetAdminBlogs
END;

-- Fetch Procedure for Admin Blog 

CREATE PROCEDURE S_GetAdminBlogs
AS
BEGIN
    SELECT
        ab.Blog_Id,
        ab.BlogTitle,
        ab.BlogSubTitle,
        ab.PostedBy,
        ab.PostedDate,
        ab.Description,
        ab.Category_Id,
        c.CategoryType,    
        ab.ImageOrVideo,
        ab.Hyperlink
    FROM 
        Tbl_AdminBlog ab
    INNER JOIN 
        Tbl_Category c ON ab.Category_Id = c.Category_Id
Where ab.EndDate > GETDATE()
    ORDER BY 
        ab.Blog_Id;
END;











