﻿using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    /// <summary>
    /// Address response model
    /// </summary>
    public class AddressResponse
    {
        /// <summary>
        /// Id of AddressResponse
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Street in address
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Address number
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Id of Address City
        /// </summary>
        public Guid CityId { get; set; }
    }
}
