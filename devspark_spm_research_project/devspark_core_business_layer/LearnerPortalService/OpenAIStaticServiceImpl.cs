using Azure;
using Azure.AI.OpenAI;
using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using Newtonsoft.Json;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.LearnerPortalService
{
    public class OpenAIStaticServiceImpl : IOpenAIStaticService
    {
        private OpenAICredentials _openAICredentials;


        public OpenAIStaticServiceImpl() { }

        public void SetOpenAICredentials(OpenAICredentials openAICredentials)
        {
            _openAICredentials = openAICredentials;
        }

        public async Task<OpenAICredentials> GetOpenAICredentials()
        {
            return _openAICredentials;
        }

        public async Task<Course> GenerateCourse(OpenAiPrompts openAiPrompts, int userId)
        {
            bool status = false;

            AzureOpenAIClient azureClient = new(new Uri(_openAICredentials.EndPoint), new AzureKeyCredential(_openAICredentials.Key));
            ChatClient chatClient = azureClient.GetChatClient(_openAICredentials.DeploymentName);

            #region Original Content
            ChatCompletion completion = chatClient.CompleteChat(
            [
                new SystemChatMessage(openAiPrompts.SystemMessage),
                new UserChatMessage(openAiPrompts.UserMessage),
                new AssistantChatMessage(openAiPrompts.AssistantMessage),
                new UserChatMessage(openAiPrompts.UserMessage),
            ]);

            string response = completion.Content[0].Text;

            if (response.Contains("``json"))
            {
                int startIndex = response.IndexOf("``json") + 6;
                int endIndex = response.LastIndexOf("```");
                response = response.Substring(startIndex, endIndex - startIndex).Trim();
            }
            #endregion

            Course course = JsonConvert.DeserializeObject<Course>(response);
            course.UserId = userId;
            course.CourseContent = response;

            return course;
        }
    }
}
