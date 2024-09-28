using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.LearnerPortal.ViewComponents
{
    public class CourseProgressViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        private readonly IDataProtector _dataProtector;
        
        public CourseProgressViewComponent(ICourseService courseService, IDataProtectionProvider dataProtectionProvider)
        {
            _courseService = courseService;
            _dataProtector = dataProtectionProvider.CreateProtector(GlobalHelpers.LearnerPortalDataProtectorSecret);
        }

        public async Task<IViewComponentResult> InvokeAsync(string courseId)
        {
            Course course = new Course();

            if (!String.IsNullOrEmpty(courseId))
            {
                int decryptedCourseId = Convert.ToInt32(_dataProtector.Unprotect(courseId));
                course = await _courseService.GetCourseByCourseId(decryptedCourseId);
            }

            return View(course);
        }
    }
}
