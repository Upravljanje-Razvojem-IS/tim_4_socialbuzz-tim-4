using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    /// <summary>
    /// Model entiteta kategorije
    /// </summary>
    public class Category
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
        /// Lista listinga koji vuku ključ
        /// </summary>
        public List<Listing> Listings { get; set; }

        #endregion

        #region Foreign keys

        /// <summary>
        /// ID roditelj kategorije
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

        /// <summary>
        /// Entitet roditelj kategorije odakle vuče ključ
        /// </summary>
        public Category ParentCategory { get; set; }

        #endregion
    }
}
