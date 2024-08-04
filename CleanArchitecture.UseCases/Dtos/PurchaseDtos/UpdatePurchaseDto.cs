using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.PurchaseDtos
{
    public class UpdatePurchaseDto
    {
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; } // Nouveau champ
        public bool IsArchived { get; set; }

    }
}
