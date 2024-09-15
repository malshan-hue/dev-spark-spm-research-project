using devspark_core_business_layer.LearnerPortalService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace devspark_core_web.Areas.LearnerPortal.ViewComponents
{
    public class CourseProgressViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        
        public CourseProgressViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string courseId)
        {
            return View();
        }
    }
}
