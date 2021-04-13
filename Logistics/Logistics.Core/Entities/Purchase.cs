using Logistics.Core.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Entities
{
    /// <summary>
    /// Represent Purchase
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// Purchase Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of item
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Item pieces
        /// </summary>
        public int Pieces { get; set; }
        /// <summary>
        /// Total weight of item
        /// </summary>
        public double TotalWeight { get; set; }
        /// <summary>
        /// Total price
        /// </summary>
        public double TotalPriceWithWeightAndDistance { get; set; }
        /// <summary>
        /// Distance for delivery
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// If of WeightRange
        /// </summary>
        public Guid WeightRangeId { get; set; }
        /// <summary>
        /// WeightRange
        /// </summary>
        public WeightRange WeightRange { get; set; }

        /// <summary>
        /// Id of DistancePrice
        /// </summary>
        public Guid DistancePriceId { get; set; }
        /// <summary>
        /// DistancePrice
        /// </summary>
        public DistancePrice DistancePrice { get; set; }

        /// <summary>
        /// Id of FromAddress
        /// </summary>
        public Guid FromAddressId { get; set; }
        /// <summary>
        /// FromAddress
        /// </summary>
        public Address FromAddess { get; set; }

        /// <summary>
        /// Id of ToAddress
        /// </summary>
        public Guid ToAddressId { get; set; }
        /// <summary>
        /// ToAddress
        /// </summary>
        public Address ToAddress { get; set; }

    }
}
