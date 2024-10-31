﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ItemDtos.ServiceDtos
{
    public class ServiceDto : ItemDto
    {
      
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsArchived { get; set; }
        public double Total { get; set; }
        // Ajoute d'autres propriétés si nécessaire
    }

}
