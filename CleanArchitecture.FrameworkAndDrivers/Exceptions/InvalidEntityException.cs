using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Exceptions
{
   public class InvalidEntityException : Exception
    {
        public InvalidEntityException()
        {
        }

        public InvalidEntityException(string message)
            : base(message)
        {
        }

        public InvalidEntityException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    
    
}
