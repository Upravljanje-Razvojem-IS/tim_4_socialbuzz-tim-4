using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models.PurchaseModels
{
    /// <summary>
    /// purchase overview model
    /// </summary>
    public class PurchaseOverview
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
