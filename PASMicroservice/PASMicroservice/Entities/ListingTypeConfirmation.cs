using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    /// <summary>
    /// Model entiteta potvrde tipa listinga
    /// </summary>
    public class ListingTypeConfirmation
    {
        #region Properties

        /// <summary>
        /// ID tipa listinga
        /// </summary>
        public int ListingTypeId { get; set; }
        
        /// <summary>
        /// Naziv tipa listinga
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
