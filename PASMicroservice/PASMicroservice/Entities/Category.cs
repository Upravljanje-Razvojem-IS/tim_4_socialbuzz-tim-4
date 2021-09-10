﻿using System;
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
        public List<ProductsAndServices> PAS { get; set; }

        // Foreign keys
        public Guid? ParentId { get; set; }
        public Category Parent { get; set; }
        public int TypeId { get; set; }
        public PASType Type { get; set; }
    }
}
