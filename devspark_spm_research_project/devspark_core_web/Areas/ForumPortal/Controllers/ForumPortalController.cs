﻿using devspark_core_business_layer.ForumPortalService.Interfaces;
using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_model.ForumPortalModels;
using devspark_core_model.LearnerPortalModels;
using devspark_core_web.Areas.ForumPortal.Models;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace devspark_core_web.Areas.ForumPortal.Controllers
{
    [Authorize]
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
            var questions = await _forumService.RetrieveQuestions();
            return View(questions);
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
           // Get the logged-in user's ID
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

           // Set the UserId on the question model
            // question.UserId = userId;


            bool status = await _forumService.InsertQuestion(question);

            if (status)
            {
                return RedirectToAction("Index", "ForumPortal");
            }
            return View();

        }

        // Display a specific question along with its answers
        [HttpGet]
        public async Task<IActionResult> ViewQuestion(int questionId)
        {
            var question = await _forumService.RetrieveQuestionById(questionId);
            var answers = await _forumService.RetrieveAnswersByQuestionId(questionId);

            var viewModel = new QuestionDetailViewModel
            {
                Question = question,
                Answers = answers
            };

            return View(viewModel);
        }
        //Display a form to create a new answer for a specific question
        [HttpGet]
        public async Task<IActionResult> CreateAnswer(int questionId)
        {
            var question = await _forumService.RetrieveQuestionById(questionId);
            var answer = new Answer { QuestionId = questionId, Question = question };
            return View(answer);
        }

         //Handle the form submission to create a new answer
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                bool status = await _forumService.InsertAnswer(answer);
                if (status)
                {
                    return RedirectToAction("Index", "ForumPortal", new { questionId = answer.QuestionId });
                }
            }
            var question = await _forumService.RetrieveQuestionById(answer.QuestionId);
            answer.Question = question;  
            return View(answer);
        }

        // ********** Update Question **********

        // Display a form to update a question
        [HttpGet]
        public async Task<IActionResult> UpdateQuestion(int questionId)
        {
            var question = await _forumService.RetrieveQuestionById(questionId);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // Handle the form submission to update a question
        [HttpPost]
        public async Task<IActionResult> UpdateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                bool status = await _forumService.UpdateQuestion(question);
                if (status)
                {
                    return RedirectToAction("ViewQuestion", "ForumPortal", new { questionId = question.QuestionId });
                }
            }
            return View(question);
        }

        // ********** Update Answer **********

        // Display a form to update an answer
        [HttpGet]
        public async Task<IActionResult> UpdateAnswer(int answerId)
        {
            var answer = await _forumService.RetrieveAnswerById(answerId);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // Handle the form submission to update an answer
        [HttpPost]
        public async Task<IActionResult> UpdateAnswer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                bool status = await _forumService.UpdateAnswer(answer);
                if (status)
                {
                    return RedirectToAction("ViewQuestion", "ForumPortal", new { questionId = answer.QuestionId });
                }
            }
            return View(answer);
        }

        // ********** Delete Question **********
        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            bool status = await _forumService.DeleteQuestion(questionId);
            if (status)
            {
                // Redirect to the index page after successful deletion
                return RedirectToAction("Index", "ForumPortal");
            }

            // Handle failure, possibly by returning an error view or message
            return View("Error");
        }

        // ********** Delete Answer **********
        [HttpPost]
        public async Task<IActionResult> DeleteAnswer(int answerId, int questionId)
        {
            bool status = await _forumService.DeleteAnswer(answerId);
            if (status)
            {
                // Redirect back to the question detail page after successful deletion
                return RedirectToAction("ViewQuestion", "ForumPortal", new { questionId });
            }

            // Handle failure, possibly by returning an error view or message
            return View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> GenerateQuestionPdf(int questionId)
        {
            var question = await _forumService.RetrieveQuestionById(questionId);
            var answers = await _forumService.RetrieveAnswersByQuestionId(questionId);

            var viewModel = new QuestionDetailViewModel
            {
                Question = question,
                Answers = answers
            };

            string htmlContent = await ITextSharpPdfHelper.RenderViewToStringAsync(this, "AnswersTemplate", viewModel);

            byte[] pdfBytes = ITextSharpPdfHelper.GeneratePdfFromHtml(htmlContent);

            var pdfName = question.Title + ".pdf";

            // Return the PDF as a file download
            return File(pdfBytes, "application/pdf", pdfName);
        }


    }
}

