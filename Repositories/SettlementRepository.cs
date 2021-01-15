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
    public class SettlementRepository : ISettlementRepository
    {
        private const string databaseName = "SettlementFomrs";
        private const string collectionName = "settlemets";
        private readonly IMongoCollection<Settlement> settlementsCollection;

        private readonly FilterDefinitionBuilder<Settlement> filterBuilder = Builders<Settlement>.Filter;

        public SettlementRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            settlementsCollection = database.GetCollection<Settlement>(collectionName);
        }

        public async Task<IEnumerable<Settlement>> GetSettlementsAsync()
        {
            return await settlementsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Settlement> GetSettlementAsync(Guid id)
        {
            return await settlementsCollection.Find(filter(id)).SingleOrDefaultAsync();
        }

        public async Task CreateSettlementAsync(Settlement settlement)
        {
            await settlementsCollection.InsertOneAsync(settlement);
        }

        public async Task DeleteSettlementAysync(Guid id)
        {
            await settlementsCollection.DeleteOneAsync(filter(id));
        }

        public async Task UpdateSettlemetAysync(Settlement settlement)
        {
            await settlementsCollection.ReplaceOneAsync(filter(settlement.Id), settlement);
        }


        private FilterDefinition<Settlement> filter(Guid id)
        {
            return filterBuilder.Eq(settlement => settlement.Id, id);
        }

    }
}
