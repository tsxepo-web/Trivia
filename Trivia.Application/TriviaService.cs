using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Trivia.Application.IServices;
using Trivia.DataAccess.Entities;

namespace Trivia.Application
{
    public class TriviaService : ITriviaService
    {
        private readonly IMongoCollection<Questions> _triviaCollection;
        public TriviaService(IMongoCollection<Questions> triviaCollection)
        {
            _triviaCollection = triviaCollection;
        }
        public async Task CreateQuestionAsync(Questions question)
        {
            await _triviaCollection.InsertOneAsync(question);
        }
        public async Task DeleteQuestionAsync(string id)
        {
            await _triviaCollection.DeleteOneAsync(x => x.Id == id);
        }
        public async Task<Questions> GetRandomQuestionAsync()
        {
            var random = new Random();
            var totalCount = await _triviaCollection.CountDocumentsAsync(FilterDefinition<Questions>.Empty);
            var randomIndex = random.Next(0, (int)totalCount);
            var randomQuestion = await _triviaCollection.Find(FilterDefinition<Questions>.Empty)
                .Skip(randomIndex)
                .Limit(1)
                .FirstOrDefaultAsync();
            return randomQuestion;
        }
        public async Task<IEnumerable<Questions>> GetQuestionsAsync()
        {
            return await _triviaCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Questions> GetQuestionAsync(string id)
        {
            return await _triviaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateQuestionAsync(Questions question)
        {
            await _triviaCollection.ReplaceOneAsync(x => x.Id == question.Id, question);
        }
    }
}