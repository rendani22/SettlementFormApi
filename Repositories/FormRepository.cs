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
    public class FormRepository : IFormRepository
    {
        private const string databaseName = "SettlementFomrs";
        private const string collectionName = "form";
        private readonly IMongoCollection<Form> formCollection;

        private readonly FilterDefinitionBuilder<Form> filterBulider = Builders<Form>.Filter;

        public FormRepository(IMongoClient mongoClient)
        {
            IMongoDatabase databse = mongoClient.GetDatabase(databaseName);
            formCollection = databse.GetCollection<Form>(collectionName);
        }

        public async Task CreateFormAsync(Form form)
        {
           await formCollection.InsertOneAsync(form);
        }

        public async Task DeleteFormAsync(Guid id)
        {
            var filter = filterBulider.Eq(form => form.Id, id);
            await formCollection.DeleteOneAsync(filter);
        }

        public async Task<Form> GetFormAsync(Guid id)
        {
            var filter = filterBulider.Eq(form => form.Id, id);
            return await formCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Form>> GetFormAsync()
        {
            return await formCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateFormAsync(Form form)
        {
            var filter = filterBulider.Eq(existingForm => existingForm.Id, form.Id);
            await formCollection.ReplaceOneAsync(filter, form);
        }

        public async Task<IEnumerable<Form>> GetSettlemetFormsAysync(Guid settlemetId)
        {
            var filter = filterBulider.Eq(form => form.SettlementId, settlemetId);
            return await formCollection.Find(filter).ToListAsync();
        }


    }
}
