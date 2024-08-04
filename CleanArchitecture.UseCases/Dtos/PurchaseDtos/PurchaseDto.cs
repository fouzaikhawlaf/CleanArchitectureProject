using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.PurchaseDtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public bool IsArchived { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
