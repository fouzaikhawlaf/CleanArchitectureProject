using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Exceptions
{
    public class WorkflowNotFoundException : Exception
    {
        public WorkflowNotFoundException(int workflowId)
            : base($"Workflow with ID {workflowId} was not found.")
        {
        }
    }
}
