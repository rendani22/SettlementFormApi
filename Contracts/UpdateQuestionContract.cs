using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record UpdateQuestionContract
    {
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public Boolean ParameterCheack { get; set; }
        public Boolean IsRequired { get; set; }
        public Boolean IsSubQuestion { get; set; }
        public int MainQuestionId { get; set; }
        public string IfAnswerIs { get; set; }
    }
}
