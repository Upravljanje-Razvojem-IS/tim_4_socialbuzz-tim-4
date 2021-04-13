using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Entities
{
    /// <summary>
    /// Represent WeightRange
    /// </summary>
    public class WeightRange
    {
        /// <summary>
        /// Id of WeightRange
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// MinimalWeight for coefficient
        /// </summary>
        public float MinimalWeight { get; set; }
        /// <summary>
        /// MaximalWeight for coefficient
        /// </summary>
        public float MaximalWeight { get; set; }
        /// <summary>
        /// Coefficient for calculating price
        /// </summary>
        public double PriceCoefficient { get; set; }
    }
}
