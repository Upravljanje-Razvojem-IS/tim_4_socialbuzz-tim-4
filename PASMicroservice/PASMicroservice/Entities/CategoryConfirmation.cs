﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    /// <summary>
    /// Model entiteta potvrde kategorije
    /// </summary>
    public class CategoryConfirmation
    {
        #region Properties

        /// <summary>
        /// ID kategorije
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Naziv kategorije
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID roditelj kategorije
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

        #endregion
    }
}
