using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.SupplierDtos
{
    public class CreateSupplierDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Phone { get; set; }
        public string? Address { get; set; }
        public PaymentTerms PaymentTerms { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public EntityType SupplierType { get; set; }
        public double TotalChiffreDAffaire { get; set; }
       
    }
}
