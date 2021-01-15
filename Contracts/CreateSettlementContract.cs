using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record CreateSettlementContract
    {
        public string Name { get; init; }
        public Boolean isLinked { get; init; }
        public string MapLink { get; init; }
    }
}
