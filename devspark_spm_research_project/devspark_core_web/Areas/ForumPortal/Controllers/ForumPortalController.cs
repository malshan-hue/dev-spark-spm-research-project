using devspark_core_business_layer.ForumPortalService.Interfaces;
using devspark_core_model.ForumPortalModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace devspark_core_web.Areas.ForumPortal.Controllers
{
    [Area("ForumPortal")]
    public class ForumPortalController : Controller
    {
        private readonly IForumService _forumService;

        public ForumPortalController(IForumService forumService)
        {
            _forumService = forumService;
        }

        //Display all questions
        [HttpGet]
         public async Task<IActionResult> Index()
        {
            //var questions = await _forumService.RetrieveQuestions();
            return View( );
        }

        // Display a form to create a new question
        [HttpGet]
        public async Task<IActionResult> CreateQuestion()
        {
            return View();
        }

        // Handle the form submission to create a new question
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(Question question)
        {
            bool status = await _forumService.InsertQuestion(question);

            if (status)
            {
                return RedirectToAction("Index", "ForumPortal");
            }
            return View();

        }

        // Display a specific question along with its answers
        //[HttpGet]
        //public async Task<IActionResult> ViewQuestion(int questionId)
        //{
        //   var question = await _forumService.RetrieveQuestionById(questionId);
        //   var answers = await _forumService.RetrieveAnswersByQuestionId(questionId);

        //  var viewModel = new QuestionDetailViewModel
        //  {
        //       Question = question,
        //       Answers = answers
        //  };

        //   return View(viewModel);
        //}

        // Display a form to create a new answer for a specific question
        //[HttpGet]
        //public IActionResult CreateAnswer(int questionId)
        //{
        //    var answer = new Answer { QuestionId = questionId };
        //   return View(answer);
        //}

        // Handle the form submission to create a new answer
        //[HttpPost]
        //public async Task<IActionResult> CreateAnswer(Answer answer)
        //{
        //    bool status = await _forumService.InsertAnswer(answer);

        //   if (status)
        //    {
        //        return RedirectToAction("ViewQuestion", new { questionId = answer.QuestionId });
        //    }

        //    return View();
        //}
    }
}

