using devspark_core_model.ForumPortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.ForumPortalService.Interfaces
{
    public interface IForumService
    {
        Task<bool> InsertAnswer(Answer answer);
        Task<bool> InsertQuestion(Question question);
        Task<ICollection<Question>> RetrieveQuestions();
        Task<Question> RetrieveQuestionById(int questionId);
        Task<ICollection<Answer>> RetrieveAnswersByQuestionId(int questionId);
    }
}
