using MongoDB.Bson;
using MongoDB.Driver;
using SettlementFormApi.Interface;
using SettlementFormApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {

        private const string databaseName = "SettlementFomrs";
        private const string collectionName = "questions";
        private readonly IMongoCollection<Questions> questionCollection;

        private readonly FilterDefinitionBuilder<Questions> filterBuilder = Builders<Questions>.Filter;

        public QuestionsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            questionCollection = database.GetCollection<Questions>(collectionName);
        }

        public async Task CreateQuestionAsync(Questions questions)
        {
           await  questionCollection.InsertOneAsync(questions);
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            await questionCollection.DeleteOneAsync(Filter(id));
        }

        public async Task<IEnumerable<Questions>> GetFormQustionsAsync(Guid FormId)
        {
            var formFilter = filterBuilder.Eq(form => form.FormId, FormId);

            return await questionCollection.Find(formFilter).SortBy(order => order.QuestionOrder).ToListAsync();
        }

        public async Task<Questions> GetQuestionAsync(Guid id)
        {
            return await questionCollection.Find(Filter(id)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Questions>> GetQustionsAsync()
        {
            return await questionCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateQuestionAsync(Questions question)
        {
            await questionCollection.ReplaceOneAsync(Filter(question.Id), question);
        }

        public async Task<int> GetNextQuestionNumber(Guid formId)
        {
            var formMaxQuestionOrder = await questionCollection.Find(x => true).SortByDescending(d => d.QuestionOrder).Limit(1).FirstOrDefaultAsync();
            return formMaxQuestionOrder.QuestionOrder + 1;
        }

        private FilterDefinition<Questions> Filter(Guid id)
        {
            return filterBuilder.Eq(question => question.Id, id);
        }

        public async Task UpdateQuestionNumbers(Questions questions)
        {
            var formQuestionOrder = await questionCollection.Find(x => true).SortByDescending(d => d.QuestionOrder).ToListAsync();
            var newQuestionOrder = questions.QuestionOrder;

            for(int i = newQuestionOrder; i <= formQuestionOrder.FirstOrDefault().QuestionOrder; i++)
            {

            }

        }
    }
}
