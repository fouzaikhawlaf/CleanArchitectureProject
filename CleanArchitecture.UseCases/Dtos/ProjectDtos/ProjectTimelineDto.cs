using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ProjectDtos
{
    public class ProjectTimelineDto
    {
        public int ProjectId { get; set; }
        public DateTime OriginalStartDate { get; set; }
        public DateTime? OriginalEndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // Peut être nullable si la date de fin n'est pas toujours connue
       public List<TimelineChangeLog> ChangeLogs { get; set; }
    }
}
