﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.ProductsAndServices
{
    public class ProductsAndServicesUpdateDto : IValidatableObject
    {
        // Properties
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public double Price { get; set; }
        public bool PriceContact { get; set; }
        public bool PriceDeal { get; set; }

        // Foreign keys

        [Required(ErrorMessage = "CategoryId is required.")]
        public Guid CategoryId { get; set; }

        // Mock properties/foreign keys
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= 0 && PriceContact == false && PriceDeal == false)
                yield return new ValidationResult(
                    "You have to set the price of product or service, or choose the option to contact or make a deal.",
                    new[] { "ProductsAndServicesUpdateDto" });

            if (CategoryId == new Guid())
                yield return new ValidationResult(
                    "CategoryId is invalid.",
                    new[] { "ProductsAndServicesUpdateDto" });

            if (UserId < 1)
                yield return new ValidationResult(
                    "UserId is invalid.",
                    new[] { "ProductsAndServicesUpdateDto" });
        }
    }
}
