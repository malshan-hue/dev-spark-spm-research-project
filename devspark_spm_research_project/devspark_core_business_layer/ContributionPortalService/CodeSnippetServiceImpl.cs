using devspark_core_business_layer.ContributionPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.ContributionPortalModels;
using devspark_core_model.ForumPortalModels;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.ContributionPortalService
{
    public class CodeSnippetServiceImpl:ICodeSnippetService
    {
        private readonly IDatabaseService _databaseService;
        public CodeSnippetServiceImpl(IDatabaseService databaseService) {
            _databaseService = databaseService;
        }

        // Insert a code snippet into the database
        public async Task<bool> InsertCodeSnippet(CodeSnippetLibrary codesnippet)
        {
            string codeSnippetJsonString = JsonConvert.SerializeObject(codesnippet);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.codeSnippetManager.InsertData("InsertCodeSnippet", codeSnippetJsonString);
            return status;
        }

        // Retrieve all the code snippets from the database
        public async Task<IEnumerable<CodeSnippetLibrary>> GetAllCodeSnippets()
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var result = dataTransactionManager.codeSnippetManager.RetrieveData("GetAllCodeSnippets");
            return result;
        }

        // Retrieve a specific code snippet by its ID
        public async Task<CodeSnippetLibrary> GetCodeSnippetById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var snippet = dataTransactionManager.codeSnippetManager.RetrieveData("GetCodeSnippetById", parameters);
            return snippet.FirstOrDefault();
        }

        // Update an existing code snippet in the database
        public async Task<bool> UpdateCodeSnippet(CodeSnippetLibrary codesnippet)
        {
            string codeSnippetJsonString = JsonConvert.SerializeObject(codesnippet);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.codeSnippetManager.UpdateData("UpdateCodeSnippet", codeSnippetJsonString);
            return status;
        }

        // Delete a code snippet by its ID
        public async Task<bool> DeleteCodeSnippet(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.codeSnippetManager.DeleteData("DeleteCodeSnippet", parameters);
            return status;
        }
    }
}
