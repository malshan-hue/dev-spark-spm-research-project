using devspark_core_model.LearnerPortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.LearnerPortalService.Interfaces
{
    public interface IOpenAIStaticService
    {
        void SetOpenAICredentials(OpenAICredentials openAICredentials);
        Task<OpenAICredentials> GetOpenAICredentials();

        Task<Course> GenerateCourse(OpenAiPrompts openAiPrompts, int userId);
    }
}
