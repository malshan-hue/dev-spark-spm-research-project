using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using devspark_core_model.SystemModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<IActionResult> CreateCourse(Course course)
        {
            var prompts = new OpenAiPrompts();
            prompts.AssistantMessage = _configuration["assistantMessage"];
            prompts.SystemMessage = _configuration["systemMessage"];
            prompts.UserMessage = _configuration["userMessage"];

            prompts.UserMessage = prompts.UserMessage.Replace("[*1*]", course.CurrentStatusEnumDisplayname);
            prompts.UserMessage = prompts.UserMessage.Replace("[*2*]", course.YearsOfExperience.ToString());
            prompts.UserMessage = prompts.UserMessage.Replace("[*3*]", course.AreaOfStudyDisplayName);
            prompts.UserMessage = prompts.UserMessage.Replace("[*4*]", course.AchivingLevelEnumDisplayName);
            prompts.UserMessage = prompts.UserMessage.Replace("[*5*]", course.StudyPeriodDisplayName);

            prompts.SystemMessage = prompts.SystemMessage.Replace("[*1*]", course.CurrentStatusEnumDisplayname);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*2*]", course.YearsOfExperience.ToString());
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*3*]", course.AreaOfStudyDisplayName);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*4*]", course.AchivingLevelEnumDisplayName);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*5*]", course.StudyPeriodDisplayName);

            var userId = 2;
            var generatedCourse = await _openAIStaticService.GenerateCourse(prompts);
            generatedCourse.AreaOfStudyEnum = course.AreaOfStudyEnum;
            generatedCourse.CurrentStatusEnum = course.CurrentStatusEnum;
            generatedCourse.YearsOfExperience = course.YearsOfExperience;
            generatedCourse.AchivingLevelEnum = course.AchivingLevelEnum;
            generatedCourse.StudyPeriodEnum = course.StudyPeriodEnum;
            generatedCourse.UserId = userId;

            bool status = await _courseService.InsertCourseWithFullContent(generatedCourse);

            if (status)
            {
                #region SYSTEM NOTIFICATION

                List<SystemNotification> systemNotifications = new List<SystemNotification>();

                SystemNotification systemNotification = new SystemNotification()
                {
                    Title = "Course Created Successfully",
                    Message = course.CourseName,
                    Time = DateTime.Now.ToString("dd/MM/yyyy"),
                    NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Success),
                    NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
                };

                systemNotifications.Add(systemNotification);

                TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

                #endregion

                return RedirectToAction("Index", "Dashboard", new { Area = "LearnerPortal" });
            }
            else
            {
                return RedirectToAction("Index", "Course", new { Area = "LearnerPortal" });
            }

            
        }
    }
}
