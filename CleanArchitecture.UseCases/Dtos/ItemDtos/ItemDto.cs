using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ItemDtos
{
    public class ItemDto
    {
        public int Id { get; set; } // ID de l'élément

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        public string Name { get; set; } = string.Empty;// Nom de l'élément

        [Required(ErrorMessage = "Le prix est requis.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0.")]
        public double Price { get; set; } // Prix unitaire

        [Required(ErrorMessage = "Le taux de TVA est requis.")]
        [Range(0, 100, ErrorMessage = "Le taux de TVA doit être compris entre 0 et 100.")]
        public double TaxRate { get; set; } // Taux de TVA en pourcentage

    }
}