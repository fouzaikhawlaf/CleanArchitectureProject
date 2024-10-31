using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ProjectDtos.TaskDtos
{
    public class TaskProgressDto
    {
        public int TaskId { get; set; }
        public double Progress { get; set; }
    }
}
