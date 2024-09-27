using devspark_core_business_layer.ContributionPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.ContributionPortalModels;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task<bool> InsertCodeSnippet(CodeSnippet codesnippet)
        {
            string codeSnippetJsonString = JsonConvert.SerializeObject(codesnippet);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.codeSnippetManager.InsertData("InsertCodeSnippet", codeSnippetJsonString);
            return status;
        }
    }
}
