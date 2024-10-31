using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class InvoiceClientRepository:GenericRepository<InvoiceClient>, IInvoiceClientRepository
    {
        public InvoiceClientRepository(AppDbContext context) : base(context)
        {
          
        }
    }
}
