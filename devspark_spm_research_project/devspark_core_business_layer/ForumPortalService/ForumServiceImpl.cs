using devspark_core_business_layer.ForumPortalService.Interfaces;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_data_access_layer;
using devspark_core_model.ForumPortalModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
            var questions = dataTransactionManager.questionDataManager.RetrieveData("RetrieveQuestions");
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
            ICollection<Answer> answers = new List<Answer>();

            // Define the parameters including the output parameter for JSON result
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@questionId", questionId),
            new SqlParameter("@jsonResult", SqlDbType.NVarChar, -1) // Output parameter for JSON
            {
            Direction = ParameterDirection.Output
                }
            };

            // Execute the stored procedure using DataTransactionManager
            using (var connection = new SqlConnection(_databaseService.GetConnectionString()))
            {
                using (var command = new SqlCommand("RetrieveAnswersByQuestionId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync(); // Execute the stored procedure

                    // Retrieve the JSON result from the output parameter
                    var jsonResult = parameters.FirstOrDefault(p => p.ParameterName == "@jsonResult")?.Value as string;

                    // Deserialize the JSON result into a collection of Answer objects
                    if (!string.IsNullOrEmpty(jsonResult))
                    {
                        answers = JsonConvert.DeserializeObject<ICollection<Answer>>(jsonResult);
                    }
                }
            }

            return answers;
        }

        public async Task<Answer> RetrieveAnswerById(int answerId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@AnswerId", answerId)
            };

            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var answer = await dataTransactionManager.answerDataManager.RetrieveSingleData<Answer>("RetrieveAnswerById", parameters);
            return answer;
        }

        public async Task<bool> UpdateQuestion(Question question)
        {
            string questionJsonString = JsonConvert.SerializeObject(question);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.questionDataManager.UpdateData("UpdateQuestion", questionJsonString);
            return status;
        }

        public async Task<bool> UpdateAnswer(Answer answer)
        {
            string answerJsonString = JsonConvert.SerializeObject(answer);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.answerDataManager.UpdateData("UpdateAnswer", answerJsonString);
            return status;
        }

        public async Task<bool> DeleteQuestion(int questionId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@QuestionId", questionId)
            };

            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.questionDataManager.DeleteData("DeleteQuestion", parameters);
            return status;
        }

        public async Task<bool> DeleteAnswer(int answerId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@AnswerId", answerId)
            };

            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            bool status = dataTransactionManager.answerDataManager.DeleteData("DeleteAnswer", parameters);
            return status;
        }







    }
}
