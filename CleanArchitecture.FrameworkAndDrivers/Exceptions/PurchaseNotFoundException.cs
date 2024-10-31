using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Exceptions
{
    public class PurchaseNotFoundException : Exception
    {
        public PurchaseNotFoundException(string message) : base(message)
        {
        }
    }
}