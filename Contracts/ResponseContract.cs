using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record ResponseContract
    {
        public Guid Id { get; init; }
        public Guid SettlementId { get; set; }
        public Guid FormId { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public string CapturedBy { get; init; }
    }
}
