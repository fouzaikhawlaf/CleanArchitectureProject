using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Leaves;
using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.Signatures.LtvVerification;

namespace CleanArchitecture.Entities.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public double Budget { get; set; }
        public double ActualCost { get; set; }
        public RiskLevel RiskLevel { get; set; }

        [ForeignKey("Manager")]
        public string? ManagerId { get; set; }
        public Manager? Manager { get; set; }

        [InverseProperty("Projects")]
        public ICollection<Employee> TeamMembers { get; set; } = new List<Employee>();
        public List<TaskProject> Tasks { get; set; } = new List<TaskProject>();

        public double CalculateRemainingBudget() => Budget - ActualCost;
        public bool IsOverdue() => EndDate.HasValue && EndDate < DateTime.Now;
    }
}
