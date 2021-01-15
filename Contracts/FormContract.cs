using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
   
        public record FormContract
        {
            public Guid Id { get; init; }
            public Guid SettlementId { get; init; }
            public int Version { get; init; }
            public DateTimeOffset CreationDate { get; init; }
            public string CreatedBy { get; init; }
        }
    
}
