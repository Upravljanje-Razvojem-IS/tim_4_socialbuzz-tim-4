using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    /// <summary>
    /// DistancePrice put model
    /// </summary>
    public class DistancePricePutBody
    {
        /// <summary>
        /// MinimalDistance for price
        /// </summary>
        public int MinimalDistance { get; set; }
        /// <summary>
        /// MaximalDistance for price
        /// </summary>
        public int MaximalDistance { get; set; }
        /// <summary>
        /// Distance price
        /// </summary>
        public double Price { get; set; }
    }
}
