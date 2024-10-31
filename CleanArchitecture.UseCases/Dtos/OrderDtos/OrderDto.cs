using CleanArchitecture.Entities.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos
{
    public  class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsArchived { get; set; }
      
        public OrderState Status { get; set; }
     
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
