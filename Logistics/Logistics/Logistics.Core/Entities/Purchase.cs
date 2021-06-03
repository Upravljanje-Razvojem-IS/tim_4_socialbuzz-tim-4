using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Id of item
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Item pieces
        /// </summary>
        [Range(0, 100)]
        [Required]
        public int Pieces { get; set; }
        /// <summary>
        /// Total weight of item
        /// </summary>
        [Range(0, 40, ErrorMessage = "Total weight must be between 0 and 40 kilograms")]
        [Required]
        public double TotalWeight { get; set; }
        /// <summary>
        /// Total price
        /// </summary>
        [Required]
        public double TotalPriceWithWeightAndDistance { get; set; }
        /// <summary>
        /// Distance for delivery
        /// </summary>
        [Required]
        public double Distance { get; set; }

        /// <summary>
        /// If of WeightRange
        /// </summary>
        [Required]
        [ForeignKey("WeightRange")]
        public Guid WeightRangeId { get; set; }
        /// <summary>
        /// WeightRange
        /// </summary>
        public WeightRange WeightRange { get; set; }

        /// <summary>
        /// Id of DistancePrice
        /// </summary>
        [Required]
        [ForeignKey("DistancePrice")]
        public Guid DistancePriceId { get; set; }
        /// <summary>
        /// DistancePrice
        /// </summary>
        public DistancePrice DistancePrice { get; set; }

        /// <summary>
        /// Id of FromAddress
        /// </summary>
        [Required]
        [ForeignKey("FromAddess")]
        public Guid FromAddressId { get; set; }
        /// <summary>
        /// FromAddress
        /// </summary>
        public Address FromAddess { get; set; }

        /// <summary>
        /// Id of ToAddress
        /// </summary>
        [Required]
        [ForeignKey("ToAddress")]
        public Guid ToAddressId { get; set; }
        /// <summary>
        /// ToAddress
        /// </summary>
        public Address ToAddress { get; set; }

    }
}
