using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Listing
{
    /// <summary>
    /// DTO model listinga
    /// </summary>
    public class ListingDto
    {
        #region Properties

        /// <summary>
        /// ID listinga
        /// </summary>
        public Guid ListingId { get; set; }

        /// <summary>
        /// Naziv listinga
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Opis listinga
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Cena u listingu
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Kontaktirati za cenu bit
        /// </summary>
        public bool PriceContact { get; set; }

        /// <summary>
        /// Dogovor za cenu bit
        /// </summary>
        public bool PriceDeal { get; set; }

        #endregion

        #region Foreign key

        /// <summary>
        /// ID kategorije
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// ID tipa listinga
        /// </summary>
        public int ListingTypeId { get; set; }

        #endregion

        #region Mock properties/foreign keys

        /// <summary>
        /// ID korisnika
        /// </summary>
        public int UserId { get; set; }

        #endregion
    }
}
