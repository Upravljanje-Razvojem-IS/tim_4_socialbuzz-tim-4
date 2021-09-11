﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    /// <summary>
    /// Model entiteta potvrde listinga
    /// </summary>
    public class ListingConfirmation
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

        #endregion

        #region Foreign keys

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
