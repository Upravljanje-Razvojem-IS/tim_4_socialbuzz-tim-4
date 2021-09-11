using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Category
{
    /// <summary>
    /// Predstavlja model kategorije
    /// </summary>
    public class CategoryDto
    {
        // Properties
        /// <summary>
        /// ID kategorije
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// Naziv kategorije
        /// </summary>
        public string Name { get; set; }

        // Foreign keys
        /// <summary>
        /// Strani ključ ID roditelj kategorije
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
