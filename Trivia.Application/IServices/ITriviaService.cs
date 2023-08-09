using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trivia.DataAccess.Entities;

namespace Trivia.Application.IServices
{
    public interface ITriviaService
    {
        Task<IEnumerable<Questions>> GetQuestionsAsync();
        Task<Questions> GetRandomQuestionAsync();
        Task<Questions> GetQuestionAsync(string id);
        Task CreateQuestionAsync(Questions question);
        Task UpdateQuestionAsync(Questions question);
        Task DeleteQuestionAsync(string id);
    }
}