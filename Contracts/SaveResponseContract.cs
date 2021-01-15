using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record SaveResponseContract(Guid FormId, Guid SettlementId);
}
