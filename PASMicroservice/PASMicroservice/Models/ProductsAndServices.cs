using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models
{
    public class ProductsAndServices
    {
        // Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool PriceContact { get; set; }
        public bool PriceDeal { get; set; }

        // Foreign keys
        public int TypeId { get; set; }
        public Guid CategoryId { get; set; }

        // Mock properties/foreign keys
        public int UserId { get; set; }
    }
}
