using CleanArchitecture.Entities.Bank;
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Transaction
{
    public class TransactionAccount
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public int TransactionId { get; set; }  // Identifiant unique pour chaque transaction.
        public DateTime TransactionDate { get; set; }  // Date et heure de la transaction.
        public double Amount { get; set; }  // Montant de la transaction.
        public string Description { get; set; } = string.Empty; // Description de la transaction.
        public TransactionType Type { get; set; }  // Type de transaction (ex : Dépôt, Retrait, etc.).
        [ForeignKey("BankAccountId")]
        public int BankAccountId { get; set; }  // Identifiant du compte bancaire concerné.
        public BankAccount? BankAccount { get; set; }  // Référence au compte bancaire associé.
        [ForeignKey("UserId")]
        public int? UserId { get; set; }  // Optionnel : Identifiant de l'utilisateur initiant la transaction.
        public Admin? User { get; set; }  // Optionnel : Référence à l'utilisateur initiateur de la transaction.
    }

}
