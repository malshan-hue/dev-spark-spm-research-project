using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.LearnerPortal.Controllers
{
    public class CourseController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenAIStaticService _openAIStaticService;
        private readonly ICourseService _courseService;

        public CourseController(IConfiguration configuration, IOpenAIStaticService openAIStaticService, ICourseService courseService)
        {
            _configuration = configuration;
            _openAIStaticService = openAIStaticService;
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CoursePrompt coursePrompt)
        {
            var prompts = new OpenAiPrompts();
            prompts.AssistantMessage = _configuration["assistantMessage"];
            prompts.SystemMessage = _configuration["systemMessage"];
            prompts.UserMessage = _configuration["userMessage"];

            prompts.UserMessage = prompts.UserMessage.Replace("[*1*]", coursePrompt.CurrentStatusEnumDisplayname);
            prompts.UserMessage = prompts.UserMessage.Replace("[*2*]", coursePrompt.YearsOfExperience.ToString());
            prompts.UserMessage = prompts.UserMessage.Replace("[*3*]", coursePrompt.AreaOfStudyDisplayName);
            prompts.UserMessage = prompts.UserMessage.Replace("[*4*]", coursePrompt.AchivingLevelEnumDisplayName);
            prompts.UserMessage = prompts.UserMessage.Replace("[*5*]", coursePrompt.StudyPeriodDisplayName);

            prompts.SystemMessage = prompts.SystemMessage.Replace("[*1*]", coursePrompt.CurrentStatusEnumDisplayname);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*2*]", coursePrompt.YearsOfExperience.ToString());
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*3*]", coursePrompt.AreaOfStudyDisplayName);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*4*]", coursePrompt.AchivingLevelEnumDisplayName);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*5*]", coursePrompt.StudyPeriodDisplayName);

            var userId = 2;

            Course course = await _openAIStaticService.GenerateCourse(prompts, userId);

            bool status = await _courseService.InsertCourseWithFullContent(course);

            if (status)
            {
                return RedirectToAction("Index", "Dashboard", new { Area = "LearnerPortal" });
            }
            else
            {
                return RedirectToAction("Index", "Course", new { Area = "LearnerPortal" });
            }

            
        }
    }
}
