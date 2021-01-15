using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record UpdateSettlemetContract
    {
        [Required]
        [MinLength(3)]
        public string Name { get; init; }
        [Required]
        public Boolean isLinked { get; init; }
        [Url]
        public string MapLink { get; init; }
    }
}
