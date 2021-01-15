using SettlementFormApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SettlementFormApi.Interface
{
    public interface IQuestionsRepository
    {
        public Task<IEnumerable<Questions>> GetFormQustionsAsync(Guid FormId);

        public Task<IEnumerable<Questions>> GetQustionsAsync();

        public Task<Questions> GetQuestionAsync(Guid id);

        public Task CreateQuestionAsync(Questions questions);

        public Task DeleteQuestionAsync(Guid id);

        public Task UpdateQuestionAsync(Questions question);

        public Task<int> GetNextQuestionNumber(Guid formId);

        public Task UpdateQuestionNumbers(Questions questions);

    }
}