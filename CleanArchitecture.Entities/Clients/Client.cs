using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Sales;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace CleanArchitecture.Entities.Clients
{
    public class Client
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public string PaymentTerms { get; set; } = string.Empty;
        public double CreditLimit { get; set; }
        public string IndustryType { get; set; } = string.Empty;
        public string Tax { get; set; } = string.Empty;
        public EntityType Type;
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }

}