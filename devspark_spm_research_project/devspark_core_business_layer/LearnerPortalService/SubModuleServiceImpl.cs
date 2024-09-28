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
    public class SubModuleServiceImpl: ISubmoduleService
    {
        private readonly IDatabaseService _databaseService;

        public SubModuleServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Submodule> GetSubmoduleById(int submoduleId)
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var subModule = dataTransactionManager.SubmoduleDataManager.RetrieveData("GetSubmoduleById", new SqlParameter[]{
                new SqlParameter("@submoduleId", submoduleId)
            }).FirstOrDefault();

            return subModule;
        }
    }
}
