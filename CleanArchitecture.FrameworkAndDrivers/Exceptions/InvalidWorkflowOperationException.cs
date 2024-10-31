using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Exceptions
{
    public class InvalidWorkflowOperationException : Exception
    {
        public InvalidWorkflowOperationException(string message)
            : base(message)
        {
        }
    }
}
