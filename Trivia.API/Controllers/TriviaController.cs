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
        [HttpPut("{id}")]
        public async Task<ActionResult<Questions>> PutQuestion(string id, [FromBody] Questions question)
        {
            if (id != question.Id) return BadRequest();
            await _triviaService.UpdateQuestionAsync(question);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> PostQuestion(Questions question)
        {
            await _triviaService.CreateQuestionAsync(question);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var questionToDelete = await _triviaService.GetQuestionAsync(id);
            if (questionToDelete.Id == null)
            {
                return NotFound();
            }
            await _triviaService.DeleteQuestionAsync(questionToDelete.Id);
            return NoContent();
        }
    }
}