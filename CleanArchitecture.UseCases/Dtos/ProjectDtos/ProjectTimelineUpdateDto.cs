using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ProjectDtos
{
    public class ProjectTimelineUpdateDto
    {
        public DateTime NewStartDate { get; set; }
        public DateTime? NewEndDate { get; set; }
        public string Reason { get; set; }
    }
}
