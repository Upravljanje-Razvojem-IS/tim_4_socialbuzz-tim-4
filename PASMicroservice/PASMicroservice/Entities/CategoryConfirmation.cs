﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    public class CategoryConfirmation
    {
        // Properties
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}