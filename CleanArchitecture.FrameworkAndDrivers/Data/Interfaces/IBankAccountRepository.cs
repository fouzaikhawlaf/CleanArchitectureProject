using CleanArchitecture.Entities.Bank;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IBankAccountRepository : IGenericRepository<BankAccount>
    {

        Task<BankAccount> GetAccountByNumberAsync(string accountNumber);
        Task<IEnumerable<BankAccount>> GetAccountsByHolderNameAsync(string accountHolderName);
    }
}
