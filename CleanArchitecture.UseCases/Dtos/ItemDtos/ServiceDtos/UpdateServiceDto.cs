using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ItemDtos.ServiceDtos
{
    public class UpdateServiceDto 
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public double? Price { get; set; } // Nullable si tu ne veux pas mettre à jour le prix

        [Range(0, 100)]
        public double? TaxRate { get; set; } // Nullable si tu ne veux pas mettre à jour le taux de TVA

        // Indiquer si le service doit être archivé
        public bool? IsArchived { get; set; }

    }

}
