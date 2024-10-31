using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Enum
{
    public enum FournisseurWorkflowSteps
    {
        InitialStep,
        RequestForQuotation,
        Evaluation,
        ContractSignature,
        Completed
    }
}
