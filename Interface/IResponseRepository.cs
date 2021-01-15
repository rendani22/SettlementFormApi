using SettlementFormApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SettlementFormApi.Interface
{
    public interface IResponseRepository
    {
        Task<IEnumerable<Response>> GetResponsesAsync();
        Task<Response> GetResponseAsync(Guid id);
        Task SaveResponseAsync(Response response, List<ResponseAnswer> responseAnswers);
        Task<IEnumerable<Response>> GetFormResponseAsync(Guid FormId);

    }
}