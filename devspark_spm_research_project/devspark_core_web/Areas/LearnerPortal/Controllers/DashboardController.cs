using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using devspark_core_model.SystemModels;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Newtonsoft.Json;

namespace devspark_core_web.Areas.LearnerPortal.Controllers
{
    [Authorize]
    [Area("LearnerPortal")]
    public class DashboardController : Controller
    {
        private readonly IMailService _mailService;
        private readonly ICourseService _courseService;
        private IDataProtector _dataProtector;

        public DashboardController(ICourseService courseService, IDataProtectionProvider dataProtectionProvider, IMailService mailService)
        {
            _courseService = courseService;
            _dataProtector = dataProtectionProvider.CreateProtector(GlobalHelpers.LearnerPortalDataProtectorSecret);
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Course> courses = new List<Course>().AsQueryable();

            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            var courseList = await _courseService.GetAllCourses(userId);
            courses = courseList.AsQueryable();

            courses.ToList().ForEach(e => 
            {
                e.EncryptedKey = _dataProtector.Protect(e.CourseId.ToString());
            });

            return View(courses);
        }

        #region TESTS
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

        [HttpPost]
        public async  Task<ActionResult> GeneratePdfFromHtmlContent()
        {
            var model = new
            {
                CustomerName = "John Doe",
                OrderNumber = "123456",
                TotalAmount = "$100.00"
            };

            string htmlContent = await ITextSharpPdfHelper.RenderViewToStringAsync(this, "OrderConfirmation", model);

            byte[] pdfBytes = ITextSharpPdfHelper.GeneratePdfFromHtml(htmlContent);

            // Return the PDF as a file download
            return File(pdfBytes, "application/pdf", "Invoice.pdf");
        }

        public async Task<IActionResult> SendEmail()
        {
            var model = new
            {
                CustomerName = "John Doe",
                OrderNumber = "123456",
                TotalAmount = "$100.00"
            };

            var emailBody = await EmailTemplateHelper.GenerateEmailBody(this, "OrderConfirmation", model);

            await _mailService.SendGoogleMail("malshan.edu@gmail.com", "Microsoft Credentials", emailBody);

            return RedirectToAction("Index", "Dashboard", new { Area = "LearnerPortal" });
        }

        #endregion
    }
}
