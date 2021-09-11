﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    public class Listing
    {
        // Properties
        public Guid ListingId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public bool? PriceContact { get; set; }
        public bool? PriceDeal { get; set; }

        // Foreign keys
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public int ListingTypeId { get; set; }
        public ListingType ListingType { get; set; }

        // Mock properties/foreign keys
        public int UserId { get; set; }
    }
}