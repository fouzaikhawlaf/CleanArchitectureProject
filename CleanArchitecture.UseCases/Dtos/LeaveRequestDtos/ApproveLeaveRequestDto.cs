using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.LeaveRequestDtos
{
    public class ApproveLeaveRequestDto
    {
        public bool IsApproved { get; set; }
        public string ManagerComment { get; set; } = string.Empty;
    }

}
