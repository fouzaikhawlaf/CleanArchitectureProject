using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class HR :Employee
    {
        // Propriétés spécifiques au rôle HR
        public string HRDepartment { get; set; } = string.Empty;
    }
}
