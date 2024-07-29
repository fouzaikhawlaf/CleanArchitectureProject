using CleanArchitecture.Entities.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FramworkAndDrivers.Data.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateSupplierPdf(IEnumerable<Supplier> suppliers);
    }
}
