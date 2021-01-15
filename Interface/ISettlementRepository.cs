using MongoDB.Driver;
using SettlementFormApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SettlementFormApi.Interface
{
    public interface ISettlementRepository
    {
        Task CreateSettlementAsync(Settlement settlement);
        Task DeleteSettlementAysync(Guid id);
        Task<Settlement> GetSettlementAsync(Guid id);
        Task<IEnumerable<Settlement>> GetSettlementsAsync();
        Task UpdateSettlemetAysync(Settlement settlement);
    }
}