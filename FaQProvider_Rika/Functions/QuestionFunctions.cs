using FaQProvider_Rika.Data;
using FaQProvider_Rika.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FaQProvider_Rika.Functions
{
    public class QuestionFunctions
    {
        private readonly QuestionDbContext _context;
        private readonly ILogger<QuestionFunctions> _logger;

        public QuestionFunctions(QuestionDbContext context, ILogger<QuestionFunctions> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Function("GetQuestions")]
        public async Task<IActionResult> GetQuestionsAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Questions")] HttpRequest req)
        {
            var questions = await _context.Questions.ToListAsync();
            return new OkObjectResult(questions);
        }

        [Function("GetQuestionById")]
        public async Task<IActionResult> GetQuestionByIdAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Questions/{id}")] HttpRequest req, int id)
        {
            var question = await _context.Questions.FindAsync(id);
            return question == null ? new NotFoundResult() : new OkObjectResult(question);
        }

        [Function("CreateQuestion")]
        public async Task<IActionResult> CreateQuestionAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Questions")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var question = JsonConvert.DeserializeObject<Question>(requestBody);

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            // Returera CreatedResult med länk och den skapade frågan
            return new OkObjectResult(question);
        }

        [Function("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestionAsync([HttpTrigger(AuthorizationLevel.Function, "put", Route = "Questions/{id}")] HttpRequest req, int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return new NotFoundResult();
            }

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedQuestion = JsonConvert.DeserializeObject<Question>(requestBody);

            // Uppdatera egenskaperna
            question.QuestionTitle = updatedQuestion.QuestionTitle;
            question.QuestionAnswer = updatedQuestion.QuestionAnswer;
            question.QuestionUrl = updatedQuestion.QuestionUrl;

            await _context.SaveChangesAsync();
            return new OkObjectResult(question);
        }

        [Function("DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestionAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Questions/{id}")] HttpRequest req, int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return new NotFoundResult();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
