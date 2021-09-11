using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Category
{
    /// <summary>
    /// DTO model kreiranja kategorije
    /// </summary>
    public class CategoryCreationDto
    {
        #region Properties

        /// <summary>
        /// Naziv kategorije
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
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
