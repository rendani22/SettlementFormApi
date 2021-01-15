using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record UpdateFormContract
    {
        [Required]
        [Range(1, 1000)]
        public int Version { get; init; }
    }
}
