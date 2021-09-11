using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    public class Category
    {
        // Properties
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public List<Listing> Listings { get; set; }

        // Foreign keys
        public Guid? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
    }
}
