using CleanArchitecture.Entities.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Produit
{
    public class Service : Item
    {
        public TimeSpan Duration { get; set; } // Durée du service
        public string Description { get; set; } = string.Empty;// Description du service
        public bool IsArchived { get; set; } = false;// Propriété pour marquer le service comme archivé
        public ICollection<InvoiceLineItem>? InvoiceLineItems { get; set; }
    }
}

