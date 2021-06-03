using System;

namespace Logistics.API.Models.PurchaseModels
{
    /// <summary>
    /// Purchase details model
    /// </summary>
    public class PurchaseDetails
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Id of details item
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Id of purchase Weight Range
        /// </summary>
        public Guid WeightRangeId { get; set; }

        public Guid DistancePriceId { get; set; }

        /// <summary>
        /// Id of purchase from address
        /// </summary>
        public Guid FromAddressId { get; set; }

        /// <summary>
        /// Id of purchase to address
        /// </summary>
        public Guid ToAddressId { get; set; }
        /// <summary>
        /// Number of item pieces
        /// </summary>
        public int Pieces { get; set; }
        /// <summary>
        /// Purchase total weight
        /// </summary>
        public double TotalWeight { get; set; }
        /// <summary>
        /// purchase total price
        /// </summary>
        public double TotalPriceWithWeightAndDistance { get; set; }
    }
}
