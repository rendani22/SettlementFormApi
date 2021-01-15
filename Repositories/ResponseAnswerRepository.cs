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
    public class ResponseAnswerRepository : IResponseAnswerRepository
    {
        private const string databaseName = "SettlementFomrs";
        private const string collectionName = "answers";
        private readonly IMongoCollection<ResponseAnswer> responseAnswerCollection;

        private readonly FilterDefinitionBuilder<ResponseAnswer> filterBuilder = Builders<ResponseAnswer>.Filter;

        public ResponseAnswerRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            responseAnswerCollection = database.GetCollection<ResponseAnswer>(collectionName);
        }

        /*Get all the respose answers in a list*/
        public async Task<IEnumerable<ResponseAnswer>> GetResponseAnswersAsync()
        {
            return await responseAnswerCollection.Find(new BsonDocument()).ToListAsync();
        }

        /*Get one response answers with the id*/
        public async Task<ResponseAnswer> GetResponseAnswerAsync(Guid id)
        {
            return await responseAnswerCollection.Find(filter(id)).SingleOrDefaultAsync();
        }
        

        /*Get the answers for one response form*/
        public async Task<IEnumerable<ResponseAnswer>> GetResponseAnswersAsync(Guid ResponseId)
        {
            var formFilter = filterBuilder.Eq(form => form.ResponseId, ResponseId);

            return await responseAnswerCollection.Find(formFilter).ToListAsync();
        }

       
        /*Save the answer*/
        public async Task SaveResponseAnswerAsync(List<ResponseAnswer> responseAnswers)
        {
            await responseAnswerCollection.InsertManyAsync(responseAnswers);
        }



        private FilterDefinition<ResponseAnswer> filter(Guid id)
        {
            return filterBuilder.Eq(settlement => settlement.Id, id);
        }

    }
}
