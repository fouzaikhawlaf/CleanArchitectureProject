using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ClientDtos
{
    public class UpdateClientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public bool IsArchived { get; set; }
        public string PaymentTerms { get; set; } = string.Empty;
        public double CreditLimit { get; set; }
        public string IndustryType { get; set; } = string.Empty;
        public string Tax { get; set; } = string.Empty;
        public EntityType Type { get; set; } // Ajout de l'énumération
        public List<Sale> Sales { get; set; } = new List<Sale>();
        public string UpdatedBy { get; set; }  // Person responsible for creating the client
    }
}
