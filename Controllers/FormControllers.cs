using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SettlementFormApi.Contracts;
using SettlementFormApi.Interface;
using SettlementFormApi.Models;
using SettlementFormApi.Repositories;

namespace SettlementFormApi.Controllers
{
    [ApiController]
    // [Route("[controller]")]
    [Route("form")]
    public class FormController : ControllerBase
    {
        private readonly IFormRepository _repository;
        private readonly ISettlementRepository _settlementRepository;
        
        public FormController(IFormRepository repository, ISettlementRepository settlementRepository)
        {
            _repository = repository;
            _settlementRepository = settlementRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FormContract>> GetFormsAsync()
        {
            var forms = (await _repository.GetFormAsync())
                        .Select(f => f.Contract());
            return forms;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormContract>> GetFormAsync(Guid id)
        {
            var form = await _repository.GetFormAsync(id);

            if(form is null){
                return NotFound();
            }
            return Ok(form.Contract());
        }

        [HttpGet("settlement/{settlemetId}")]
        public async Task<ActionResult<FormContract>> GetSettlemetForms(Guid settlemetId)
        {
            var forms = await _repository.GetSettlemetFormsAysync(settlemetId);

            if (forms is null)
            {
                return NotFound();
            }

            return Ok(forms);
        }

        // Post /form
        [HttpPost]
        public async Task<ActionResult<FormContract>> CreateFormAsync(CreateFormContract formContract)
        {
            var existingSettlement = await _settlementRepository.GetSettlementAsync(formContract.SettlementId);

            if(existingSettlement is null)
            {
                return NotFound();
            }
            
            Form form = new()
            {
                Id = Guid.NewGuid(),
                SettlementId = formContract.SettlementId,
                Version = 1,
                CreationDate = DateTimeOffset.UtcNow,
                CreatedBy = formContract.CreatedBy
            };

           await _repository.CreateFormAsync(form);

            return CreatedAtAction(nameof(GetFormAsync), new { id = form.Id }, form.Contract());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFormAsync(Guid id)
        {
            var existingForm = await _repository.GetFormAsync(id);

            if(existingForm is null)
            {
                return NotFound();
            }

            Form updatedForm = existingForm with
            {
                Version = existingForm.Version + 1
            };

            await _repository.UpdateFormAsync(updatedForm);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteForm(Guid id)
        {
            var existingForm = await _repository.GetFormAsync(id);

            if (existingForm is null)
            {
                return NotFound();
            }

            await _repository.DeleteFormAsync(id);
            return NoContent();
        }
    }
}