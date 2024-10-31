using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ProjectDtos
{
    public class TimelineChangeLog
    {
        public DateTime ChangeDate { get; set; }
        public string Reason { get; set; }
        public DateTime OldStartDate { get; set; }
        public DateTime? OldEndDate { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime? NewEndDate { get; set; }
    }
}
