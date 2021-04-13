using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    /// <summary>
    /// WeightRange post model
    /// </summary>
    public class WeightRangePostBody
    {
        /// <summary>
        /// MinimalWeight for price coefficient
        /// </summary>
        public float MinimalWeight { get; set; }
        /// <summary>
        /// MaximalWeight for price coefficient
        /// </summary>
        public float MaximalWeight { get; set; }
        /// <summary>
        /// coefficient for calculating price
        /// </summary>
        public double PriceCoefficient { get; set; }
    }
}
