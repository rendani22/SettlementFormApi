using SettlementFormApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SettlementFormApi.Interface
{
    public interface IResponseAnswerRepository
    {
        Task SaveResponseAnswerAsync(List<ResponseAnswer> responseAnswers);
        Task<IEnumerable<ResponseAnswer>> GetResponseAnswersAsync();
        Task<ResponseAnswer> GetResponseAnswerAsync(Guid id);
        Task<IEnumerable<ResponseAnswer>> GetResponseAnswersAsync(Guid ResponseId);
    }
}