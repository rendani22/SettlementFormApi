using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Models
{
    public record Settlement
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Boolean isLinked { get; set; }
        public string MapLink { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public string CreatedBy { get; init; }
    }
}
