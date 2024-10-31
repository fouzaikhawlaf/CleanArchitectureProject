using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto
{
    public class BonDeReceptionItemDto
    {
        public int Id { get; set; } // Identifiant unique de l'item
        public int ItemId { get; set; } // Identifiant de l'item (produit ou service)
        public int ReceivedQuantity { get; set; } // Quantité reçue
        public double UnitPrice { get; set; } // Prix unitaire
        public double TotalPrice => ReceivedQuantity * UnitPrice; // Prix total calculé
    }
}
