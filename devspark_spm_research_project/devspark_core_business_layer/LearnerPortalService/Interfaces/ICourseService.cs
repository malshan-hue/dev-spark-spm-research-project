using devspark_core_model.LearnerPortalModels;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.LearnerPortalService.Interfaces
{
    public interface ICourseService
    {
        Task<bool> InsertCourseWithFullContent(Course course);
        Task<IEnumerable<Course>> GetAllCourses(int userId);
    }
}
