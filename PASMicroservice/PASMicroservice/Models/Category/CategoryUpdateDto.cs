
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Category
{
    public class CategoryUpdateDto : IValidatableObject
    {
        // Properties
        [Required(ErrorMessage = "Id is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        
        // Foreign keys
        public Guid? ParentCategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CategoryId == ParentCategoryId)
                yield return new ValidationResult(
                    "Category can't have same values for Id and ParentCategoryId.",
                    new[] { "CategoryUpdateDto" });
        }
    }
}
