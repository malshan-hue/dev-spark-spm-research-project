using devspark_core_business_layer.DeveloperPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.DeveloperPortalModels;
using devspark_core_model.SystemModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.DeveloperPortalService
{
    public class CreateDevSpaceServiceImpl:ICreateDevSpace
    {
        private readonly IDatabaseService _databaseService;
        public CreateDevSpaceServiceImpl(IDatabaseService databaseService) {
            _databaseService = databaseService;
        }

        public async Task<bool> CreateDevSpace(Folder folder)
        {
            string userJsonString = JsonConvert.SerializeObject(folder);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.InsertData("CreateDevSpace", userJsonString);
            return status;
        }
    }
}
