﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.SalesDtos
{
    public class UpdateSaleDto
    {
        public int Id { get; set; } // Identifiant de la vente à mettre à jour
        public int InvoiceId { get; set; }
        public int ClientId { get; set; }
        public DateTime SaleDate { get; set; }
        public double TotalAmount { get; set; }
        public string? Status { get; set; } // Vous pouvez ajouter d'autres propriétés si nécessaire
        public bool IsArchived { get; set; } // Si vous avez besoin de mettre à jour cet attribut
        public string ProductName { get; set; } // Assurez-vous que cette propriété existe
    }
}
