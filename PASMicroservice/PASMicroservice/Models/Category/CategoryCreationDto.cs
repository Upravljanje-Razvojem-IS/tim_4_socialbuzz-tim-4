using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Category
{
    public class CategoryCreationDto
    {
        // Properties
        /// <summary>
        /// Naziv kategorije
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        // Foreign keys
        /// <summary>
        /// Strani ključ ID roditelj kategorije
        /// </summary>
        public Guid? ParentCategoryId { get; set; }
    }
}
