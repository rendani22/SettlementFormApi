using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SettlementFormApi.Contracts
{
    public record ResponseAnswerContract(Guid Id, string Answer, Guid FormId);
    
}
