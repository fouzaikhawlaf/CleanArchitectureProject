using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.BankDtos
{
    public class BankAccountDto
    {
        public string AccountHolderName { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public double InitialDeposit { get; set; }
        public TransactionType TransactionType { get; set; } // Debit or Credit
    }
}
