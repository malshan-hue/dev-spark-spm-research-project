using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using devspark_core_model.SystemModels;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace devspark_core_web.Areas.LearnerPortal.Controllers
{
    [Authorize]
    [Area("LearnerPortal")]
    public class CourseController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IOpenAIStaticService _openAIStaticService;
        private readonly ICourseService _courseService;
        private IDataProtector _dataProtector;

        public CourseController(IConfiguration configuration, IOpenAIStaticService openAIStaticService, ICourseService courseService, IDataProtectionProvider dataProtectionProvider)
        {
            _configuration = configuration;
            _openAIStaticService = openAIStaticService;
            _courseService = courseService;
            _dataProtector = dataProtectionProvider.CreateProtector(GlobalHelpers.LearnerPortalDataProtectorSecret);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
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
            prompts.UserMessage = prompts.UserMessage.Replace("[*2*]", course.YearsOfExperienceEnumDisplayname);
            prompts.UserMessage = prompts.UserMessage.Replace("[*3*]", course.AreaOfStudyDisplayName);
            prompts.UserMessage = prompts.UserMessage.Replace("[*4*]", course.AchivingLevelEnumDisplayName);
            prompts.UserMessage = prompts.UserMessage.Replace("[*5*]", course.StudyPeriodDisplayName);

            prompts.SystemMessage = prompts.SystemMessage.Replace("[*1*]", course.CurrentStatusEnumDisplayname);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*2*]", course.YearsOfExperienceEnumDisplayname);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*3*]", course.AreaOfStudyDisplayName);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*4*]", course.AchivingLevelEnumDisplayName);
            prompts.SystemMessage = prompts.SystemMessage.Replace("[*5*]", course.StudyPeriodDisplayName);

            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));

            var generatedCourse = await _openAIStaticService.GenerateCourse(prompts);
            generatedCourse.AreaOfStudyEnum = course.AreaOfStudyEnum;
            generatedCourse.CurrentStatusEnum = course.CurrentStatusEnum;
            generatedCourse.YearsOfExperienceEnum = course.YearsOfExperienceEnum;
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
                    Message = generatedCourse.CourseName,
                    Time = DateTime.Now.ToString("dd/MM/yyyy"),
                    NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Success),
                    NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
                };

                systemNotifications.Add(systemNotification);

                TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

                #endregion

                return Json(new { redirectToUrl = Url.Action("Index", "Dashboard", new { Area = "LearnerPortal" }) });
            }
            else
            {
                #region SYSTEM NOTIFICATION

                List<SystemNotification> systemNotifications = new List<SystemNotification>();

                SystemNotification systemNotification = new SystemNotification()
                {
                    Title = "Error creating course",
                    Message = "Created course didn't saved properly",
                    Time = DateTime.Now.ToString("dd/MM/yyyy"),
                    NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Danger),
                    NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
                };

                systemNotifications.Add(systemNotification);

                TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

                #endregion

                return RedirectToAction("CreateCourse", "Course", new { Area = "LearnerPortal" });
            }

            
        }

        [HttpGet]
        public async Task<IActionResult> ViewCourse(string courseId)
        {
            int decryptedCourseId = Convert.ToInt32(_dataProtector.Unprotect(courseId));
            Course course = await _courseService.GetCourseByCourseId(decryptedCourseId);
            course.EncryptedKey = courseId;
            course.Modules.ToList().ForEach(e => { e.EncryptedKey = _dataProtector.Protect(e.ModuleId.ToString()); });
            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> CourseProgress(string courseId)
        {
            return ViewComponent("CourseProgress", new {courseId = courseId });
        }

        [HttpGet]
        public async Task<IActionResult> CourseProgressData(string courseId)
        {
            int decryptedCourseId = Convert.ToInt32(_dataProtector.Unprotect(courseId));
            CourseProgress courseProgress = await _courseService.GetCourseProgressByCourseId(decryptedCourseId);
            return Json(courseProgress);
        }

        [HttpPost]
        public async Task<ActionResult> GenerateCoursePdf(string courseId)
        {
            int decryptedCourseId = Convert.ToInt32(_dataProtector.Unprotect(courseId));
            Course course = await _courseService.GetCourseByCourseId(decryptedCourseId);

            string htmlContent = await ITextSharpPdfHelper.RenderViewToStringAsync(this, "CourseTemplate", course);

            byte[] pdfBytes = ITextSharpPdfHelper.GeneratePdfFromHtml(htmlContent);

            var pdfName = course.CourseName + ".pdf";

            // Return the PDF as a file download
            return File(pdfBytes, "application/pdf", pdfName);
        }

    }
}
