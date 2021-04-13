﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    /// <summary>
    /// WeightRange response model
    /// </summary>
    public class WeightRangeResponse
    {
        /// <summary>
        /// weight range id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// MinimalWeight for price coefficient
        /// </summary>
        public float MinimalWeight { get; set; }
        /// <summary>
        /// MaximalWeight for price coefficient
        /// </summary>
        public float MaximalWeight { get; set; }
    }
}
