using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto.InvoiceClientDtos
{
    public class InvoiceStatisticsDto
    {
        public double TotalInvoicedAmount { get; set; }
        public int TotalInvoices { get; set; }
        public double TotalPaidAmount { get; set; }
        public int TotalPaidInvoices { get; set; }
        public double TotalPendingAmount { get; set; }
        public int TotalPendingInvoices { get; set; }
    }
}
