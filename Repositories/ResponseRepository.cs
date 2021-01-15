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


    public class ResponseRepository : IResponseRepository
    {
        private const string databaseName = "SettlementFomrs";
        private const string collectionName = "responses";
        private readonly IMongoCollection<Response> responsesCollection;
        IResponseAnswerRepository _responseAnswerRepository;



        private readonly FilterDefinitionBuilder<Response> filterBuilder = Builders<Response>.Filter;

        public ResponseRepository(IMongoClient mongoClient, IResponseAnswerRepository responseAnswerRepository)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            responsesCollection = database.GetCollection<Response>(collectionName);
            _responseAnswerRepository = responseAnswerRepository;
        }

        public async Task<IEnumerable<Response>> GetResponsesAsync()
        {
            return await responsesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Response> GetResponseAsync(Guid id)
        {
            return await responsesCollection.Find(filter(id)).SingleOrDefaultAsync();
        }

        public async Task SaveResponseAsync(Response response, List<ResponseAnswer> responseAnswers)
        {
            await responsesCollection.InsertOneAsync(response);

            await _responseAnswerRepository.SaveResponseAnswerAsync(responseAnswers);
        }

        public async Task<IEnumerable<Response>> GetFormResponseAsync(Guid FormId)
        {
            var formFilter = filterBuilder.Eq(form => form.FormId, FormId);

            return await responsesCollection.Find(formFilter).ToListAsync();
        }


        private FilterDefinition<Response> filter(Guid id)
        {
            return filterBuilder.Eq(res => res.Id, id);
        }
    }
}
