using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.ProductsAndServices
{
    public class ProductsAndServicesConfirmationDto
    {
        // Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Foreign keys
        public int TypeId { get; set; }
        public Guid CategoryId { get; set; }

        // Mock properties/foreign keys
        public int UserId { get; set; }
    }
}
