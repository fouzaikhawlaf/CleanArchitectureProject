using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Sales;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.Entities.Invoices;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace CleanArchitecture.Entities.Clients
{
    public class Client
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public string PaymentTerms { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Preferences { get; set; } = string.Empty; 
        public double CreditLimit { get; set; }
        public string IndustryType { get; set; } = string.Empty;
        public string Tax { get; set; } = string.Empty;
        public EntityType Type;
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<OrderClient> OrderClients { get; set; } = new List<OrderClient>();
        public ICollection<Devis> Devises { get; set; } = new List<Devis>();
        public ICollection<DeliveryNote> DeliveryNotes { get; set; } = new List<DeliveryNote>();
        public ICollection<InvoiceClient>? InvoiceClients { get; set; }
    }
}

