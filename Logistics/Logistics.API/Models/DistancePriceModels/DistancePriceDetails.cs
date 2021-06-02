using System;

namespace Logistics.API.Models.DistancePriceModels
{
    /// <summary>
    /// Distance price details model
    /// </summary>
    public class DistancePriceDetails
    {
        /// <summary>
        /// DistancePrice details model Id
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
