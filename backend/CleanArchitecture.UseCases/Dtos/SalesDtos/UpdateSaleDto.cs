using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.SalesDtos
{
    public class UpdateSaleDto
    {
        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public bool IsArchived { get; set; } = false;
    }
}
