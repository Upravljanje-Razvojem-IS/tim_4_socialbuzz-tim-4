using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    public class Category
    {
        // Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId {get; set; }
        
        // Foreign keys
        public int TypeId { get; set; }
    }
}
