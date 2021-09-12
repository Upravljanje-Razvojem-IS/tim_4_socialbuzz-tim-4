using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.ListingType
{
    /// <summary>
    /// DTO model za tip listinga
    /// </summary>
    public class ListingTypeDto
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
