namespace ORS_website.Server.Models.Common
{
    public class Constant
    {
        public class Schema
        {
            public const string DBO = "dbo";
        }
        public class StoredProcedure
        {
            #region Insert
            public const string S_INS_EXPERIENCE = "S_InsExperience";
            public const string S_INS_SKILLS = "S_InsSkills";
            public const string S_INS_QUESTIONARY = "S_InsQuestionary";
            public const string S_INS_CATEGORY = "S_InsCategory";
            public const string S_INS_ADMIN_BLOG = "S_InsAdminBlog";
            public const string S_INS_ADMIN_CAREER = "S_InsAdminCareer";
            public const string S_INS_APPLY_CAREER = "S_InsApplyCareer";
            #endregion

            #region Fetch
            public const string S_GET_EXPERIENCE = "S_GetExperience";
            public const string S_GET_SKILLS = "S_GetSkills";
            public const string S_GET_QUESTIONARY = "S_GetQuestionary";
            public const string S_GET_CATEGORY = "S_GetCategory";
            public const string S_GET_ADMIN_BLOGS = "S_GetAdminBlogs";
            public const string S_GET_CAREERS = "S_GetCareers";
            //public const string S_GET_APPLY_CAREER = "S_GetApplyCareer";
            #endregion
        }
    }
}
