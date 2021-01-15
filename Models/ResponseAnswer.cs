using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Models
{
    public record ResponseAnswer
    {
        public Guid Id { get; init; }
        public string Answer { get; init; }
        public Guid ResponseId { get; init; }

    }
}
