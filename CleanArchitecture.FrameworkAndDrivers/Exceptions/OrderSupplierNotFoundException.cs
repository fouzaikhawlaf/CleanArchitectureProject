using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Exceptions
{
    /// <summary>
    /// Représente une exception qui se produit lorsque le fournisseur d'une commande n'est pas trouvé.
    /// </summary>
    public class OrderSupplierNotFoundException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="OrderSupplierNotFoundException"/>.
        /// </summary>
        public OrderSupplierNotFoundException() : base("Le fournisseur de commande n'a pas été trouvé.")
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="OrderSupplierNotFoundException"/>.
        /// </summary>
        /// <param name="message">Le message d'erreur.</param>
        public OrderSupplierNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="OrderSupplierNotFoundException"/>.
        /// </summary>
        /// <param name="message">Le message d'erreur.</param>
        /// <param name="innerException">L'exception interne.</param>
        public OrderSupplierNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

