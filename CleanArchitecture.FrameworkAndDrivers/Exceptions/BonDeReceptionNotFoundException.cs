using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Exceptions
{
    public class BonDeReceptionNotFoundException : Exception
    {
        public BonDeReceptionNotFoundException() : base("Bon de réception introuvable.")
        {
        }

        public BonDeReceptionNotFoundException(string message) : base(message)
        {
        }

        public BonDeReceptionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

