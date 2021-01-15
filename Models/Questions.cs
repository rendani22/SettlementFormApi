using System;

namespace SettlementFormApi.Models{

    public record Questions{
        public Guid Id { get; init; }    
        public Guid FormId { get; init; }
        public string Question { get; init; }
        public string QuestionType { get; init; }
        public Boolean ParameterCheack { get; init; }
        public Boolean IsRequired { get; init; }
        public int Version { get; init; }
        public Boolean IsSubQuestion { get; init; }
        public int MainQuestionId { get; init; }
        public string IfAnswerIs { get; init; }
        public DateTime CreationDate { get; init; }
        public string CreatedBy { get; init; }
        public int QuestionOrder { get; init; }
    }
}