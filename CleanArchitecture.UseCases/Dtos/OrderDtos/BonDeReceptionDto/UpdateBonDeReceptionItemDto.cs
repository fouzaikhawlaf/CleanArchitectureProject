using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto
{
    public class UpdateBonDeReceptionItemDto
    {
        public int ItemId { get; set; }  // Ajoutez cette propriété
        public int ReceivedQuantity { get; set; }  // Ajoutez d'autres propriétés selon vos besoins
        public double UnitPrice { get; set; } // Prix unitaire
    }
}
