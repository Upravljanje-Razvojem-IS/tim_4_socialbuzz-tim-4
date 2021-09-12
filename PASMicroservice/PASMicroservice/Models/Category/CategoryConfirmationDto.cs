using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Category
{
    /// <summary>
    /// DTO model potvrde kategorije
    /// </summary>
    public class CategoryConfirmationDto
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

        #endregion

        #region Foreign keys

        /// <summary>
        /// ID roditelj kategorije
        /// </summary>
        public Guid? ParentCategoryId { get; set; }

        #endregion
    }
}
