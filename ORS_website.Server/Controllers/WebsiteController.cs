using Microsoft.AspNetCore.Mvc;
using ORS_website.Server.Contracts;
using static ORS_website.Server.Models.ViewModels.WebsiteViewModel;

namespace ORS_website.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ORSWebsiteController : ControllerBase
    {
        private readonly IWebsiteService _websiteService;

        public ORSWebsiteController(IWebsiteService websiteService)
        {
            _websiteService = websiteService;
        }

        [HttpGet("Experiences")]
        public async Task<IActionResult> GetExperiences()
        {
            var result = await _websiteService.GetExperiences();

            return Ok(result);
        }

        [HttpGet("Skills")]
        public async Task<IActionResult> GetSkills()
        {
            var result = await _websiteService.GetSkills();

            return Ok(result);
        }

        [HttpGet("Category")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await _websiteService.GetCategory();

            return Ok(result);
        }

        [HttpPost("Admin/InsertCareers")]
        public async Task<IActionResult> InsertAdminCareers(AdminCareer adminCareer)
        {
            var result = await _websiteService.InsertAdminCareers(adminCareer);

            return Ok();
        }

        [HttpPost("Admin/InsertBlog")]
        public async Task<IActionResult> InsertAdminBlog(AdminBlog adminBlog)
        {
            var result = await _websiteService.InsertAdminBlog(adminBlog);

            return Ok();
        }

        [HttpPost("ApplyCareer")]
        public async Task<IActionResult> ApplyCareer(ApplyCareer applyCareer)
        {
            var result = await _websiteService.ApplyCareer(applyCareer);

            return Ok();
        }

        [HttpPost("InsertQuestionary")]
        public async Task<IActionResult> InsertQuestionary(ApplyQuestionary applyQuestionary)
        {
            var result = await _websiteService.InsertQuestionary(applyQuestionary);

            return Ok();
        }
        [HttpPost("InsertSkill")]
        public async Task<IActionResult> InsertSkill(string skillName)
        {
            var result = await _websiteService.InsertSkill(skillName);

            return Ok();
        }

        [HttpPost("InsertExperience")]
        public async Task<IActionResult> InsertExperience(string experienceRange)
        {
            var result = await _websiteService.InsertExperience(experienceRange);

            return Ok();
        }
        [HttpPost("InsertCategory")]
        public async Task<IActionResult> InsertCategory(string categoryType)
        {
            var result = await _websiteService.InsertCategory(categoryType);

            return Ok();
        }

        [HttpGet("GetCountryCodes")]
        public async Task<IActionResult> GetCountryCodes()
        {
            var result = await _websiteService.GetCountryCodes();

            return Ok(result);
        }

    }

}
