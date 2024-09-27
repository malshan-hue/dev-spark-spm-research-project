using devspark_core_business_layer.DeveloperPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.DeveloperPortalModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<bool> CreateDevSpace(devspark_core_model.DeveloperPortalModels.Folder folder)
        {
            string userJsonString = JsonConvert.SerializeObject(folder);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.InsertData("CreateDevSpace", userJsonString);
            return status;
        }

        public async Task<bool> CreateNewFile(FileModel file)
        {
            string userJsonString = JsonConvert.SerializeObject(file);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.InsertData("CreateNewFile", userJsonString);
            return status;
        }

        public async Task<bool> CreateNewFolder(devspark_core_model.DeveloperPortalModels.Folder folder)
        {
            string userJsonString = JsonConvert.SerializeObject(folder);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.InsertData("CreateNewFolder", userJsonString);
            return status;
        }

        public async Task<bool> DeleteFile(int fileid)
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.DeleteData("DeleteFile", new SqlParameter[]{
                new SqlParameter("@fileid", fileid)
            });
            return status;
        }

        public async Task<bool> DeleteFolder(int folderid)
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.DeleteData("DeleteFolder", new SqlParameter[]{
                new SqlParameter("@folderid", folderid)
            });
            return status;
        }

        public async Task<IEnumerable<devspark_core_model.DeveloperPortalModels.Folder>> GetDevSpaces()
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());

            // Call the generic RetrieveData method and specify Folder as the TEntity type
            var folders = dataTransactionManager.devSpaceManager.RetrieveData("GetDevSpaces");

            // Return the retrieved folders or an empty list if nothing is found
            return folders;
        }
        
        public async Task<bool> UpdateDevSpace(devspark_core_model.DeveloperPortalModels.Folder folder)
        {
            string userJsonString = JsonConvert.SerializeObject(folder);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.InsertData("UpdateDevSpace", userJsonString);
            return status;
        }

        public async Task<bool> UpdateFile(FileModel file)
        {
            string userJsonString = JsonConvert.SerializeObject(file);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.UpdateData("UpdateFile", userJsonString);
            return status;
        }

        public async Task<bool> UpdateFileInfo(FileModel file)
        {
            string userJsonString = JsonConvert.SerializeObject(file);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.UpdateData("UpdateFileInfo", userJsonString);
            return status;
        }

        public async Task<bool> UpdateFolder(devspark_core_model.DeveloperPortalModels.Folder folder)
        {
            string userJsonString = JsonConvert.SerializeObject(folder);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.devSpaceManager.UpdateData("UpdateFolder", userJsonString);
            return status;
        }
    }
}
