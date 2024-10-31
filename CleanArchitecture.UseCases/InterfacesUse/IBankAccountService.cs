using CleanArchitecture.Entities.Bank;
using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IBankAccountService
    {
        Task<BankAccount> OpenAccountAsync(string accountHolderName, AccountType accountType, double initialDeposit);
        Task<BankAccount> GetAccountByNumberAsync(string accountNumber);
        Task<IEnumerable<BankAccount>> GetAccountsByHolderNameAsync(string accountHolderName);
    }
}
