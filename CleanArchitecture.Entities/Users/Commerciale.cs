using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class Commerciale: Employee
    {
        // Propriétés spécifiques au rôle Commercial
        public string SalesRegion { get; set; } = string.Empty;
    }
}
