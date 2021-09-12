using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    /// <summary>
    /// Model entiteta tipa listinga
    /// </summary>
    public class ListingType
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

        /// <summary>
        /// Lista listinga koji vuku ključ
        /// </summary>
        public List<Listing> Listings { get; set; }

        #endregion
    }
}
