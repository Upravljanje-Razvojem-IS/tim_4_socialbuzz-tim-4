using System;

namespace Logistics.API.Models.PurchaseModels
{
    public class PurchaseConfirmation
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Id of overview item
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
    }
}
