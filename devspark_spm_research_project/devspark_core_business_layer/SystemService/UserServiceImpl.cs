using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.SystemModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.SystemService
{
    public class UserServiceImpl : IUserService
    {
        private readonly IDatabaseService _databaseService;

        public UserServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> InsertUser(EntraIdUser entraIdUser)
        {
            string userJsonString = JsonConvert.SerializeObject(entraIdUser);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.EntraIdUserDataManager.InsertData("InsertUser", userJsonString);
            return status;
        }
    }
}
