﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.SalesDtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public bool IsArchived { get; set; } = false;
    }
}
