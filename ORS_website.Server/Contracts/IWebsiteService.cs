using static ORS_website.Server.Models.ViewModels.WebsiteViewModel;

namespace ORS_website.Server.Contracts
{
    public interface IWebsiteService
    {
        Task<IEnumerable<Experiences>> GetExperiences();
        Task<IEnumerable<Skills>> GetSkills();
        Task<IEnumerable<Category>> GetCategory();
        Task<bool> InsertAdminCareers(AdminCareer adminCareer);
        Task<bool> InsertAdminBlog(AdminBlog adminBlog);
        Task<bool> ApplyCareer(ApplyCareer applyCareer);
        Task<bool> InsertQuestionary(ApplyQuestionary applyQuestionary);
        Task<bool> InsertSkill(string skillName);
        Task<bool> InsertExperience(string experienceRange);
        Task<bool> InsertCategory(string categoryType);
    }
}
