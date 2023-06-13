using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trivia.Application.IServices;
using Trivia.DataAccess.Entities;

namespace Trivia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TriviaController : ControllerBase
    {
        private readonly ITriviaService _triviaService;
        public TriviaController(ITriviaService triviaService)
        {
            _triviaService = triviaService;
        }
        [HttpGet]
        public async Task<IEnumerable<Questions>> GetQuestions()
        {
            return await _triviaService.GetQuestionsAsync();
        }
        [HttpGet("random")]
        public async Task<ActionResult<Questions>> GetQuestion()
        {
            return await _triviaService.GetRandomQuestionAsync();
        }
        [HttpPut("{question}")]
        public async Task<ActionResult<Questions>> PutQuestion([FromBody] Questions item, string question)
        {
            if (question != item.Question) return BadRequest();
            await _triviaService.UpdateQuestionAsync(item);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> PostQuestion(Questions question)
        {
            await _triviaService.CreateQuestionAsync(question);
            return NoContent();
        }
        [HttpDelete("{question}")]
        public async Task<IActionResult> DeleteQuestion(string question)
        {
            var questionToDelete = await _triviaService.GetQuestionAsync(question);
            if (questionToDelete == null)
            {
                return NotFound();
            }
            await _triviaService.DeleteQuestionAsync(questionToDelete.Question!);
            return NoContent();
        }
    }
}