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

        public async Task<string> GenerateCourse(OpenAiPrompts openAiPrompts)
        {
            var courseContent = "";
            bool status = false;

            AzureOpenAIClient azureClient = new(new Uri(_openAICredentials.EndPoint), new AzureKeyCredential(_openAICredentials.Key));
            ChatClient chatClient = azureClient.GetChatClient(_openAICredentials.DeploymentName);

            try
            {
                courseContent = await GeneratedCourseResponce(chatClient, openAiPrompts);
                Course course = JsonConvert.DeserializeObject<Course>(courseContent);

            }catch (Exception ex)
            {
                courseContent = await GeneratedCourseResponce(chatClient, openAiPrompts);
            }

            return courseContent;
        }

        public async Task<string> GeneratedCourseResponce(ChatClient chatClient, OpenAiPrompts openAiPrompts)
        {

            try
            {
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

                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
