using CleanArchitecture.Entities.Bank;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FrameworksAndDrivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
    {
        private readonly AppDbContext _context;

        public BankAccountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BankAccount> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.BankAccounts.FirstOrDefaultAsync(ba => ba.AccountNumber == accountNumber);
        }

        public async Task<IEnumerable<BankAccount>> GetAccountsByHolderNameAsync(string accountHolderName)
        {
            return await _context.BankAccounts
                                 .Where(ba => ba.AccountHolderName.Contains(accountHolderName))
                                 .ToListAsync();
        }
    }
    
}
