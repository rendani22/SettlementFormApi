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
    public class QuestionsController : ControllerBase
    {

        IQuestionsRepository _questions;
        IFormRepository _formRepository;

        public QuestionsController(IQuestionsRepository questions, IFormRepository formRepository)
        {
            _questions = questions;
            _formRepository = formRepository;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<QuestionContract>> GetFormQuestionsAsync(Guid id)
        {

            return (await _questions.GetFormQustionsAsync(id))
                   .Select(q => q.QuestionContract());
        }

        [HttpGet]
        public async Task<IEnumerable<QuestionContract>> GetQuestionsAsync()
        {

            return (await _questions.GetQustionsAsync())
                   .Select(questions => questions.QuestionContract());
           
        }

        [HttpPost]
        public async Task<ActionResult<QuestionContract>> CreateQuestionAsync(CreateQuestionContact createQuestionContact)
        {

            var exstingForm = await _formRepository.GetFormAsync(createQuestionContact.FormId);

            if ( exstingForm is null)
            {
                return NotFound();
            }

            Questions questions = new()
            {
                Id = Guid.NewGuid(),
                FormId = createQuestionContact.FormId,
                Question = createQuestionContact.Question,
                QuestionType = createQuestionContact.QuestionType,
                ParameterCheack = createQuestionContact.ParameterCheack,
                IsRequired = createQuestionContact.IsRequired,
                Version = 1,
                IsSubQuestion = createQuestionContact.IsSubQuestion,
                MainQuestionId = createQuestionContact.MainQuestionId,
                IfAnswerIs = createQuestionContact.IfAnswerIs,
                CreationDate = DateTime.Now,
                CreatedBy = "Admin",
                QuestionOrder = await _questions.GetNextQuestionNumber(createQuestionContact.FormId)
            };

            await _questions.CreateQuestionAsync(questions);

            return CreatedAtAction(nameof(GetFormQuestionsAsync), new { id = questions.Id }, questions.QuestionContract());
        }

        [HttpPut]
        public async Task<ActionResult> UpdateQuestionAsync(Guid id, UpdateQuestionContract updateQuestionContract)
        {
            var existingQuestion = await _questions.GetQuestionAsync(id);
            var exstingForm = await _formRepository.GetFormAsync(existingQuestion.FormId);

            if(existingQuestion is null || exstingForm is null)
            {
                return NotFound();
            }

            Questions UpdateQuestion = existingQuestion with
            {
                Question = updateQuestionContract.Question,
                QuestionType = updateQuestionContract.QuestionType,
                ParameterCheack = updateQuestionContract.ParameterCheack,
                IsRequired = updateQuestionContract.IsRequired,
                IsSubQuestion = updateQuestionContract.IsSubQuestion,
                MainQuestionId = updateQuestionContract.MainQuestionId,
                IfAnswerIs = updateQuestionContract.IfAnswerIs,
                Version = existingQuestion.Version + 1
            };

            await _questions.UpdateQuestionAsync(UpdateQuestion);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteQuestionAsync(Guid id)
        {
            var existingQuestion = await _questions.GetQuestionAsync(id);

            if(existingQuestion is null)
            {
                return NotFound();
            }

            await _questions.DeleteQuestionAsync(id);
            return NoContent();
        }

    }
}
