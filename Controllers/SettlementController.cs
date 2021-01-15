using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SettlementFormApi.Contracts;
using SettlementFormApi.Interface;
using SettlementFormApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettlementController : ControllerBase
    {
        private readonly ISettlementRepository _settlement;

        public SettlementController(ISettlementRepository settlement)
        {
            _settlement = settlement;
        }

        [HttpGet]
        public async Task<IEnumerable<SettlementContract>> GetSettlementsAsync()
        {
            return (await _settlement.GetSettlementsAsync())
                              .Select(settlement => settlement.SettlementContract());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SettlementContract>> GetSettlementAsync(Guid id)
        {
            var settlement = await _settlement.GetSettlementAsync(id);

            if (settlement is null)
            {
                return NotFound();
            }

            return Ok(settlement);
        }

        [HttpPost]
        public async Task<ActionResult<SettlementContract>> CreateSettlementAsync(CreateSettlementContract settlementContract)
        {
            Settlement settlement = new()
            {
                Id = Guid.NewGuid(),
                Name = settlementContract.Name,
                isLinked = settlementContract.isLinked,
                MapLink = settlementContract.MapLink,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "Admin"
            };

            await _settlement.CreateSettlementAsync(settlement);

            return CreatedAtAction(nameof(GetSettlementAsync), new { id = settlement.Id }, settlement.SettlementContract());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SettlementContract>> UpdateSettlemet(Guid id, UpdateSettlemetContract updateSettlemetContract)
        {
            var existingForm = await _settlement.GetSettlementAsync(id);

            if (existingForm is null)
            {
                return NotFound();
            }

            Settlement UpdateSettlement = existingForm with
            {
                Name = updateSettlemetContract.Name,
                isLinked = updateSettlemetContract.isLinked,
                MapLink = updateSettlemetContract.MapLink
            };

            await _settlement.UpdateSettlemetAysync(UpdateSettlement);

            return CreatedAtAction(nameof(GetSettlementAsync), new { id = id }, UpdateSettlement.SettlementContract());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSettlemet(Guid id)
        {
            var existingForm = await _settlement.GetSettlementAsync(id);

            if(existingForm is null)
            {
                return NotFound();
            }

            await _settlement.DeleteSettlementAysync(id);
            return NoContent();
        }


    }

}
