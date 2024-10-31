using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Orders
{
    // Domain/Entities/ExitNote.cs
    public class ExitNote
    {
        public Guid Id { get; set; }
        public Order? Order { get; set; }
        public DateTime DispatchDate { get; set; }
        public bool IsDispatched { get; set; }
    }

}
