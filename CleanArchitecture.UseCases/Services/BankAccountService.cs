using CleanArchitecture.Entities.Bank;
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.InterfacesUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly HttpClient _httpClient;

        public BankAccountService(IBankAccountRepository bankAccountRepository, HttpClient httpClient)
        {
            _bankAccountRepository = bankAccountRepository;
            _httpClient = httpClient;
        }

        public async Task<BankAccount> OpenAccountAsync(string accountHolderName, AccountType accountType, double initialDeposit)
        {
            var newAccount = new BankAccount
            {
                AccountHolderName = accountHolderName,
                AccountType = accountType,
                Balance = initialDeposit,
                Sold = initialDeposit, // Initialize Sold with the initial deposit
                DateOpened = DateTime.Now,
                AccountNumber = GenerateAccountNumber(),
                TransactionType = TransactionType.Credit // Opening an account is typically a credit transaction
            };

            await _bankAccountRepository.AddAsync(newAccount);
            return newAccount;
        }

        public async Task<BankAccount> GetAccountByNumberAsync(string accountNumber)
        {
            var account = await _bankAccountRepository.GetAccountByNumberAsync(accountNumber);

            if (account == null)
            {
                account = await FetchExternalBankAccountAsync(accountNumber);

                if (account != null)
                {
                    await _bankAccountRepository.AddAsync(account);
                }
            }

            return account;
        }

        public async Task<IEnumerable<BankAccount>> GetAccountsByHolderNameAsync(string accountHolderName)
        {
            return await _bankAccountRepository.GetAccountsByHolderNameAsync(accountHolderName);
        }

        private string GenerateAccountNumber()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
        }

        private async Task<BankAccount> FetchExternalBankAccountAsync(string accountNumber)
        {
            var externalApiUrl = $"https://api.externalbank.com/accounts/{accountNumber}";

            try
            {
                var externalAccount = await _httpClient.GetFromJsonAsync<BankAccount>(externalApiUrl);

                if (externalAccount != null)
                {
                    externalAccount.Id = 0;
                    externalAccount.DateOpened = DateTime.Now;
                }

                return externalAccount;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
    
    
}
