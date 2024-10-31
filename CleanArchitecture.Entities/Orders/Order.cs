using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Entities.Orders
{
    public abstract class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Définit la date de commande par défaut à la date actuelle

        public bool IsArchived { get; set; } = false; // Champ pour l'archivage

        public OrderState Status { get; set; }

       

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
