using SettlementFormApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SettlementFormApi.Interface
{
    public interface IFormRepository
    {
        Task<Form> GetFormAsync(Guid id);
        Task<IEnumerable<Form>> GetFormAsync();
        Task CreateFormAsync(Form form);
        Task UpdateFormAsync(Form form);
        Task DeleteFormAsync(Guid id);

        Task<IEnumerable<Form>> GetSettlemetFormsAysync(Guid settlemetId);
    }
}