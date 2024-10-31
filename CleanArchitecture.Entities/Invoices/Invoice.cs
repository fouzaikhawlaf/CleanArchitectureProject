using CleanArchitecture.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Enum;

namespace CleanArchitecture.Entities.Invoices
{
    public abstract class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }  // Primary key
        public string? InvoiceNumber { get; set; }  // Unique invoice number
        public DateTime InvoiceDate { get; set; }  // Date of invoice creation
        public double TaxRate { get; set; }
        public double TotalAmountWithTax { get; set; }
        public double TotalAmount { get; set; }  // Total invoice amount
        public bool IsPaid { get; set; }  // Payment status
        public ICollection<InvoiceLineItem> Items { get; set; } = new List<InvoiceLineItem>();
    }
}
