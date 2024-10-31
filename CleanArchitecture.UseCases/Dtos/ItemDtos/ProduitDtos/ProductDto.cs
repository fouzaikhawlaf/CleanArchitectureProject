﻿using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos
{
    public class ProductDto : ItemDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty; // Initialiser avec une chaîne vide
        public string Description { get; set; } = string.Empty; // Initialiser avec une chaîne vide
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public bool IsArchived { get; set; } = false;
        public ProductType ProductType { get; set; }
    }
}