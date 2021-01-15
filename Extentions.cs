using SettlementFormApi.Contracts;
using SettlementFormApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Controllers
{
    public static class Extentions
    {
        public static FormContract Contract(this Form f)
        {
            return new FormContract
            {
                Id = f.Id,
                SettlementId = f.SettlementId,
                CreatedBy = f.CreatedBy,
                CreationDate = f.CreationDate,
                Version = f.Version
            };
        }

        public static QuestionContract QuestionContract(this Questions q)
        {
            return new QuestionContract
            {
                Id = q.Id,
                FormId = q.FormId,
                Question = q.Question,
                QuestionType = q.QuestionType,
                ParameterCheack = q.ParameterCheack,
                IsRequired = q.IsRequired,
                Version = q.Version,
                IsSubQuestion = q.IsSubQuestion,
                MainQuestionId = q.MainQuestionId,
                IfAnswerIs = q.IfAnswerIs,
                CreationDate = q.CreationDate,
                CreatedBy = q.CreatedBy,
                QuestionOrder = q.QuestionOrder
            };
        }

        public static SettlementContract SettlementContract(this Settlement s)
        {
            return new SettlementContract
            {
                Id = s.Id,
                Name = s.Name,
                isLinked = s.isLinked,
                MapLink = s.MapLink,
                CreatedDate = s.CreatedDate,
                CreatedBy = s.CreatedBy
            };
        }

        public static ResponseContract ResponseContract(this Response r)
        {
            return new ResponseContract
            {
                Id = r.Id,
                SettlementId = r.SettlementId,
                CapturedBy = r.CapturedBy,
                CreatedDate = r.CreatedDate,
                FormId = r.FormId
            };
        }

        public static ResponseAnswerContract ResponseAnswerContract(this ResponseAnswerContract a)
        {
            return new ResponseAnswerContract(a.Id, a.Answer, a.FormId);
        }

    }
}
