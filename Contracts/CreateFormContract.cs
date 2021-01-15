using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record CreateFormContract
    {
        [Required]
        public Guid SettlementId { get; init; }
        [Required]
        public string CreatedBy { get; init; }
    }
}
