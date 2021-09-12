
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Category
{
    /// <summary>
    /// DTO model za izmenu kategorije
    /// </summary>
    public class CategoryUpdateDto : IValidatableObject
    {
        #region Properties

        /// <summary>
        /// ID kategorije
        /// </summary>
        [Required(ErrorMessage = "Id is required.")]
        public Guid CategoryId { get; set; }

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

        /// <summary>
        /// Validacija unetih vrednosti
        /// </summary>
        /// <remarks>
        /// Ako je id roditelj kategorije isti kao id kategorije daje grešku u validaciji
        /// </remarks>
        /// <returns>Rezultat validacije</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CategoryId == ParentCategoryId)
                yield return new ValidationResult(
                    "Category can't have same values for Id and ParentCategoryId.",
                    new[] { "CategoryUpdateDto" });
        }
    }
}
