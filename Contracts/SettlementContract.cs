using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record SettlementContract
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Boolean isLinked { get; init; }
        public string MapLink { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public string CreatedBy { get; init; }
    }
}
