using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ProjectDtos
{
    public class ProjectBudgetDto
    {
        public int ProjectId { get; set; }
        public double OriginalBudget { get; set; }
        public double CurrentBudget { get; set; }
        public double ActualCost { get; set; }
        public double Variance { get; set; }
        public double Budget { get; set; }
    }
}
