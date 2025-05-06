namespace ORS_website.Server.Models.ViewModels
{
    public class WebsiteViewModel
    {
        public class Experiences
        {
            public int E_Id { get; set; }
            public string ExperienceRange { get; set; }
        }

        public class Skills
        {
            public int S_Id { get; set; }
            public string Skill { get; set; }
        }

        public class Category
        {
            public int C_Id { get; set; }
            public string CategoryType { get; set; }
        }

        public class AdminCareer
        {
            public string JobTitle { get; set; }
            public string Description { get; set; }
            public int Skill_Id { get; set; }
            public int Experience_Id { get; set; }
            public int NoOfOpenings { get; set; }
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }

        }

        public class AdminBlog
        {
            public string BlogTitle { get; set; }
            public string? SubTitle { get; set; }
            public string PostedBy { get; set; }
            public DateOnly PostedDtae { get; set; }
            public string Description { get; set; }
            public int CategoryId { get; set; }
            public IFormFile ImageOrVideo { get; set; }
            public string HyperLink { get; set; }
        }

        public class ApplyCareer
        {
            public string JobTitle { get; set; }
            public string Job_Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNo { get; set; }
            public string PrevOrganisation { get; set; }
            public string PrevOrgLocation { get; set; }
            public string CurrentCTC { get; set; }
            public string ExceptedCTC { get; set; }
            public string ReasonForJoin { get; set; }
            public IFormFile Resume { get; set; }
            public IFormFile CoverLetter { get; set; }
            public bool TermsAndConditions { get; set; }
        }

        public class ApplyQuestionary
        {
            public string Name { get; set; }
            public string PhoneNo { get; set; }
            public string Email { get; set; }
            public string Questions { get; set; }
        }

        public class CountryCodes
        {
            public string Country { get; set; }
            public string Code { get; set; }
            public string Iso { get; set; }
        }

    }
}
