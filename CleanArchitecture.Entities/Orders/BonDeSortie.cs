using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Orders
{
    public class BonDeSortie
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public int CommandeId { get; set; }
        public List<BonDeSortieItem> Items { get; set; } = new List<BonDeSortieItem>();

        public BonDeSortie(int commandeId)
        {
            CommandeId = commandeId;
            DateCreation = DateTime.UtcNow;
        }
    }
}
