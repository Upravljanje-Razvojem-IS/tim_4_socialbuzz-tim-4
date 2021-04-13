using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Entities
{
    /// <summary>
    /// Represent DistancePrice
    /// </summary>
    public class DistancePrice
    {
        /// <summary>
        /// If of DistancePrice
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// MinimalDistance for price
        /// </summary>
        public int MinimalDistance { get; set; }
        /// <summary>
        /// MaximalDistance for price
        /// </summary>
        public int MaximalDistance { get; set; }
        /// <summary>
        /// Price for distance
        /// </summary>
        public double Price { get; set; }
    }
}
