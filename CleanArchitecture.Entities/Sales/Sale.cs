using CleanArchitecture.Entities.Produit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Clients;

namespace CleanArchitecture.Entities.Sales
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
       
        public Product Product { get; set; } = null!;
        [Required]
        public decimal Amount { get; set; }
        public bool IsArchived { get; set; } = false;
        public decimal TotalAmount { get; set; } // Ajouté
        public string ClientName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
    }
    
}
