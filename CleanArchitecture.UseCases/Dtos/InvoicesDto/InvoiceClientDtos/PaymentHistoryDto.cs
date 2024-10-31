using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto.InvoiceClientDtos
{
    public class PaymentHistoryDto
    {
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; } // e.g. Credit Card, Bank Transfer
        public string Status { get; set; } // e.g. Paid, Pending
    }
}
