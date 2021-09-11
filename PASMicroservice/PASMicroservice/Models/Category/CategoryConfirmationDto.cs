using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Category
{
    public class CategoryConfirmationDto
    {
        // Properties
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        
        // Foreign keys
        public Guid? ParentCategoryId { get; set; }
    }
}
