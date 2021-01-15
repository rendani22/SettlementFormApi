using System;

namespace SettlementFormApi.Models
{
    public record Form
    {
        public Guid Id { get; init; }
        public Guid SettlementId { get; init; }
        public int Version { get; init; }
        public DateTimeOffset CreationDate { get; init; }
        public string CreatedBy { get; init; }
    }
}