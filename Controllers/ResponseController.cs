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
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseRepository _response;
        private readonly IFormRepository _form;


        public ResponseController(IResponseRepository responseRepository, IFormRepository formRepository)
        {
            _response = responseRepository;
            _form = formRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<ResponseContract>> GetResponsesAsync()
        {
            return (await _response.GetResponsesAsync())
                    .Select(response => response.ResponseContract());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseContract>> GetResponseAsync(Guid id)
        {
            var response = await _response.GetResponseAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("{FormId}")]
        public async Task<ActionResult<ResponseContract>> SaveResponseAsync(Guid FormId, List<SaveResponseAnswerContract> saveResponseAnswerContracts)
        {
            var existingForm = await _form.GetFormAsync(FormId);

            if (existingForm is null)
            {
                return NotFound();
            }

            Response response = new()
            {
                Id = Guid.NewGuid(),
                FormId = FormId,
                CreatedDate = DateTimeOffset.UtcNow,
                SettlementId = existingForm.SettlementId,
                CapturedBy = "Admin"
            };

            List<ResponseAnswer> saveResponses = new();

            foreach (var answer in saveResponseAnswerContracts)
            {
                ResponseAnswer insertIntoList = new()
                {
                    Id = Guid.NewGuid(),
                    Answer = answer.answer,
                    ResponseId = response.Id
                };

                saveResponses.Add(insertIntoList);
            }

            await _response.SaveResponseAsync(response, saveResponses);

            return CreatedAtAction(nameof(GetResponseAsync), new { id = response.Id }, response.ResponseContract());
        }
    }
}
