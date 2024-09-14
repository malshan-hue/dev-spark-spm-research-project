using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using devspark_core_model.SystemModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace devspark_core_web.Areas.LearnerPortal.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICourseService _courseService;
        private IDataProtector _dataProtector;

        public DashboardController(ICourseService courseService, IDataProtectionProvider dataProtectionProvider)
        {
            _courseService = courseService;
            _dataProtector = dataProtectionProvider.CreateProtector("courseprotect");
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Course> courses = new List<Course>().AsQueryable();

            int userId = 2;
            var courseList = await _courseService.GetAllCourses(userId);
            courses = courseList.AsQueryable();

            courses.ToList().ForEach(e => 
            {
                e.EncryptedKey = _dataProtector.Protect(e.CourseId.ToString());
            });

            return View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> CheckNotification()
        {
            #region SYSTEM NOTIFICATION

            List<SystemNotification> systemNotifications = new List<SystemNotification>();

            SystemNotification systemNotification = new SystemNotification()
            {
                Title = "Course Created Successfully",
                Message = "Test Notification",
                Time = DateTime.Now.ToString("dd/MM/yyyy"),
                NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Primary),
                NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
            };
            SystemNotification systemNotification1 = new SystemNotification()
            {
                Title = "Course Created Successfully",
                Message = "Test Notification",
                Time = DateTime.Now.ToString("dd/MM/yyyy"),
                NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Success),
                NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
            };
            SystemNotification systemNotification2 = new SystemNotification()
            {
                Title = "Course Created Successfully",
                Message = "Test Notification",
                Time = DateTime.Now.ToString("dd/MM/yyyy"),
                NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Warning),
                NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
            };
            SystemNotification systemNotification3 = new SystemNotification()
            {
                Title = "Course Created Successfully",
                Message = "Test Notification",
                Time = DateTime.Now.ToString("dd/MM/yyyy"),
                NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Dark),
                NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
            };
            SystemNotification systemNotification4 = new SystemNotification()
            {
                Title = "Course Created Successfully",
                Message = "Test Notification",
                Time = DateTime.Now.ToString("dd/MM/yyyy"),
                NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Secondary),
                NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight)
            };

            systemNotifications.Add(systemNotification);
            systemNotifications.Add(systemNotification1);
            systemNotifications.Add(systemNotification2);
            systemNotifications.Add(systemNotification3);
            systemNotifications.Add(systemNotification4);

            TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

            #endregion
            return RedirectToAction("Index", "Dashboard", new { Area = "LearnerPortal" });
        }
    }
}
