using System;

namespace Logistics.API.Models.DistancePriceModels
{
    /// <summary>
    /// DistancePrice overview model
    /// </summary>
    public class DistancePriceOverview
    {
        /// <summary>
        /// DistancePrice overview model Id
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
        /// Distance price
        /// </summary>
        public double Price { get; set; }
    }
}
