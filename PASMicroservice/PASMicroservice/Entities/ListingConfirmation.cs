﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    public class ListingConfirmation
    {
        // Properties
        public Guid ListingId { get; set; }
        public string Name { get; set; }
        // Foreign keys
        public Guid CategoryId { get; set; }
        public int ListingTypeId { get; set; }

        // Mock properties/foreign keys
        public int UserId { get; set; }
    }
}