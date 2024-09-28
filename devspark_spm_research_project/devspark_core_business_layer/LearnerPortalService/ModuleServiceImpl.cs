using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.LearnerPortalModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.LearnerPortalService
{
    public class ModuleServiceImpl : IModuleService
    {
        private readonly IDatabaseService _databaseService;

        public ModuleServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Module> GetModuleByModuleId(int moduleId)
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var module = dataTransactionManager.ModuleDataManager.RetrieveData("GetModuleByModuleId", new SqlParameter[]{
                new SqlParameter("@moduleId", moduleId)
            }).FirstOrDefault();

            return module;
        }
    }
}
