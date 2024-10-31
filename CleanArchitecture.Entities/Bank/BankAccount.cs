using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Transaction;

namespace CleanArchitecture.Entities.Bank
{
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string ? AccountHolderName { get; set; }
        public double Balance { get; set; }
        public double Sold { get; set; } // Represents the current sold amount
        public DateTime DateOpened { get; set; }
        public AccountType AccountType { get; set; }
        public TransactionType TransactionType { get; set; } // Debit or Credit
        public ICollection<TransactionAccount> Transactions { get; set; }
    }
}
