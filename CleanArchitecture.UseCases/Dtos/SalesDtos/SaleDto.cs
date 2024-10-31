using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.SalesDtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime SaleDate { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; } // Si vous avez besoin d'afficher le statut
        public bool IsArchived { get; set; } // Si vous avez besoin d'afficher l'état archivé
        public string ProductName { get; set; } // Assurez-vous que cette propriété existe

    }

}
