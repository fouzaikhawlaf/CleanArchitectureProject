using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto
{
    public class CreateBonDeReceptionItemDto
    {
        public int ItemId { get; set; } // Identifiant de l'item
        public int ReceivedQuantity { get; set; } // Quantité reçue
        public double UnitPrice { get; set; } // Prix unitaire
    }
}
