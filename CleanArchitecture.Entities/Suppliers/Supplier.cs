using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Purchases;

namespace CleanArchitecture.Entities.Suppliers
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Phone { get; set; }
        public string? Address { get; set; }
        public bool IsArchived { get; set; } = false;
        public PaymentTerms  PaymentTerms { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public double  TotalChiffreDAffaire { get; set; }
        public EntityType SupplierType { get; set; }
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    }
}
