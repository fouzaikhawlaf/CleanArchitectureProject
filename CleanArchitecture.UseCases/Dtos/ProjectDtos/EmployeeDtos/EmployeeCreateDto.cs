using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ProjectDtos.EmployeeDtos
{
    public class EmployeeCreateDto
    {
        public string? FirstName { get; set; }
        public string ? LastName { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public DateTime HireDate { get; set; }
    }
}
