using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.Category
{
    public class CategoryCreationDto
    {
        // Properties
        public string Name { get; set; }
        public Guid? ParentId { get; set; }

        // Foreign keys
        public int TypeId { get; set; }
    }
}
