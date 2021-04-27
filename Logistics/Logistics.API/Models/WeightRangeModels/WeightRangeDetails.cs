using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models.WeightRangeModels
{
    /// <summary>
    /// weightrange details model
    /// </summary>
    public class WeightRangeDetails
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
        /// <summary>
        /// Coefficient for calculating price
        /// </summary>
        public double PriceCoefficient { get; set; }
    }
}
