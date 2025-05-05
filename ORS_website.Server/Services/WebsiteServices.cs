using ORS_website.Server.Contracts;
using static ORS_website.Server.Models.Common.Constant;
using static ORS_website.Server.Models.ViewModels.WebsiteViewModel;

namespace ORS_website.Server.Services
{
    public class WebsiteService : IWebsiteService
    {
        private readonly IDbRepository _dbRepository;

        public WebsiteService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        #region Insert
        public async Task<bool> ApplyCareer(ApplyCareer applyCareer)
        {
            object parameters = new
            {
                P_JobTitle = applyCareer.JobTitle,
                P_JobId = applyCareer.Job_Id,
                P_FullName = applyCareer.FullName,
                P_Email = applyCareer.Email,
                P_PhoneNo = applyCareer.PhoneNo,
                P_PrevOrganisation = applyCareer.PrevOrganisation,
                P_PrevOrgLocation = applyCareer.PrevOrgLocation,
                P_CurrentCTC = applyCareer.CurrentCTC,
                P_ExpectedCTC = applyCareer.ExceptedCTC,
                P_ReasonForJoin = applyCareer.ReasonForJoin,
                P_Resume = applyCareer.Resume,
                P_CoverLetter = applyCareer.CoverLetter,
                P_TermsAndConditions = applyCareer.TermsAndConditions
            };

            return await _dbRepository.ExecuteProcedureV2Async<bool>($"{Schema.DBO}.{StoredProcedure.S_INS_APPLY_CAREER}", parameters);
        }
        #endregion

        #region Fetch
        public async Task<IEnumerable<Category>> GetCategory() => await _dbRepository.ExecuteProcedureV2Async<IEnumerable<Category>>($"{Schema.DBO}.{StoredProcedure.S_GET_CATEGORY}");

        public async Task<IEnumerable<Experiences>> GetExperiences() => await _dbRepository.ExecuteProcedureV2Async<IEnumerable<Experiences>>($"{Schema.DBO}.{StoredProcedure.S_GET_EXPERIENCE}");

        public async Task<IEnumerable<Skills>> GetSkills() => await _dbRepository.ExecuteProcedureV2Async<IEnumerable<Skills>>($"{Schema.DBO}.{StoredProcedure.S_GET_SKILLS}");

        public async Task<bool> InsertAdminBlog(AdminBlog adminBlog)
        {
            object parameters = new
            {
                P_BlogTitle = adminBlog.BlogTitle,
                P_SubTitle = adminBlog.SubTitle,
                P_PostedBy = adminBlog.PostedBy,
                P_PostedDate = adminBlog.PostedDtae,
                P_Description = adminBlog.Description,
                P_Category = adminBlog.CategoryId,
                P_ImageOrVideo = adminBlog.ImageOrVideo,
                P_HyperLink = adminBlog.HyperLink
            };

            return await _dbRepository.ExecuteProcedureV2Async<bool>($"{Schema.DBO}.{StoredProcedure.S_INS_ADMIN_BLOG}", parameters);
        }

        public async Task<bool> InsertAdminCareers(AdminCareer adminCareer)
        {
            object parameters = new
            {
                P_JobTitle = adminCareer.JobTitle,
                P_Description = adminCareer.Description,
                P_Skill_Id = adminCareer.Skill_Id,
                P_Experience_Id = adminCareer.Experience_Id,
                P_NoOfOpenings = adminCareer.NoOfOpenings,
                P_StartDate = adminCareer.StartDate,
                P_EndDate = adminCareer.EndDate,
            };

            return await _dbRepository.ExecuteProcedureV2Async<bool>($"{Schema.DBO}.{StoredProcedure.S_INS_ADMIN_CAREER}", parameters);
        }

        public async Task<bool> InsertQuestionary(ApplyQuestionary applyQuestionary)
        {
            object parameters = new
            {
                P_Name = applyQuestionary.Name,
                P_PhoneNo = applyQuestionary.PhoneNo,
                P_Email = applyQuestionary.Email,
                P_Questions = applyQuestionary.Questions,
            };

            return await _dbRepository.ExecuteProcedureV2Async<bool>($"{Schema.DBO}.{StoredProcedure.S_INS_QUESTIONARY}", parameters);
        }
        public async Task<bool> InsertExperience(string experienceRange)
        {
            object parameters = new
            {
                P_Experience = experienceRange,
            };

            return await _dbRepository.ExecuteProcedureV2Async<bool>($"{Schema.DBO}.{StoredProcedure.S_INS_EXPERIENCE}", parameters);
        }

        public async Task<bool> InsertSkill(string skillName)
        {
            object parameters = new
            {
                P_SkillName = skillName
            };

            return await _dbRepository.ExecuteProcedureV2Async<bool>($"{Schema.DBO}.{StoredProcedure.S_INS_SKILLS}", parameters);
        }
        public async Task<bool> InsertCategory(string categoryType)
        {
            object parameters = new
            {
                P_CategoryType = categoryType
            };

            return await _dbRepository.ExecuteProcedureV2Async<bool>($"{Schema.DBO}.{StoredProcedure.S_INS_CATEGORY}", parameters);
        }
    }
    #endregion
}