﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Models.ListingType
{
    public class ListingTypeCreationDto
    {
        // Properties
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}