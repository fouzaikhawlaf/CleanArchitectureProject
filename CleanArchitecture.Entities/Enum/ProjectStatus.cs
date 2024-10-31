using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Enum
{
    // ValueObjects/ProjectStatus.cs
    public enum ProjectStatus
    {
        NotStarted,
        InProgress,
        OnHold,
        Completed,
        Cancelled
    }
}
