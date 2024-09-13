using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.LearnerPortalModels;
using devspark_core_model.SystemModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.LearnerPortalService
{
    public class CourseServiceImpl : ICourseService
    {
        private readonly IDatabaseService _databaseService;

        public CourseServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> InsertCourseWithFullContent(Course course)
        {
            string userJsonString = JsonConvert.SerializeObject(course);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.CourseDataManager.InsertData("InsertCourseWithFullContent", userJsonString);
            return status;
        }

        public async Task<IEnumerable<Course>> GetAllCourses(int userId)
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var course = dataTransactionManager.CourseDataManager.RetrieveData("GetAllCourses", new SqlParameter[]{
                new SqlParameter("@userId", userId)
            });

            return course;
        }
    }
}
