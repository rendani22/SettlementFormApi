using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record QuestionContract
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public Boolean ParameterCheack { get; set; }
        public Boolean IsRequired { get; set; }
        public int Version { get; set; }
        public Boolean IsSubQuestion { get; set; }
        public int MainQuestionId { get; set; }
        public string IfAnswerIs { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public int QuestionOrder { get; init; }
    }
}
