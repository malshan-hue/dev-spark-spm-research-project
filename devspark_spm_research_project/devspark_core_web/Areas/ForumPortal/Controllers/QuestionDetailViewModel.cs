using devspark_core_model.ForumPortalModels;

namespace devspark_core_web.Areas.ForumPortal.Controllers
{
    public class QuestionDetailViewModel
    {
        public Question Question { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
