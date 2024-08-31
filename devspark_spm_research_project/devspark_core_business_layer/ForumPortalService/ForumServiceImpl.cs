using devspark_core_business_layer.ForumPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.ForumPortalModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.ForumPortalService
{
    public class ForumServiceImpl : IForumService
    {

        private readonly IDatabaseService _databaseService;

        public ForumServiceImpl(IDatabaseService databaseService) { 
            _databaseService = databaseService;
        }
        public async Task<bool> InsertAnswer(Answer answer)
        {
            string answerJsonString = JsonConvert.SerializeObject(answer);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.answerDataManager.InsertData("InsertAnswer", answerJsonString);
            return status;
        }

        public async Task<bool> InsertQuestion(Question question)
        {
            string questionJsonString = JsonConvert.SerializeObject(question);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.questionDataManager.InsertData("InsertQuestion", questionJsonString);
            return status;
        }



        public async Task<ICollection<Question>> RetrieveQuestions()
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var questions = dataTransactionManager.questionDataManager.RetrieveData<Question>("RetrieveQuestions");
            return questions;
        }


        public async Task<Question> RetrieveQuestionById(int questionId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@QuestionId", questionId)
            };

            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var question = await dataTransactionManager.questionDataManager.RetrieveSingleData<Question>("RetrieveQuestionById", parameters);
            return question;
        }

        public async Task<ICollection<Answer>> RetrieveAnswersByQuestionId(int questionId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@QuestionId", questionId)
            };

            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var answers = dataTransactionManager.answerDataManager.RetrieveData<Answer>("RetrieveAnswersByQuestionId", parameters);
            return answers;
        }

    }
}
