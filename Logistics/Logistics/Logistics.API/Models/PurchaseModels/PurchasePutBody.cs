using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.API.Models.PurchaseModels
{
    /// <summary>
    /// Purchase Put model
    /// </summary>
    public class PurchasePutBody
    {
        /// <summary>
        /// Id of purchase item
        /// </summary>
        [Required]
        public Guid ItemId { get; set; }

        /// <summary>
        /// Number of item pieces
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pieces value must be greater then 0")]
        public int Pieces { get; set; }

        /// <summary>
        /// Id of purchase from address
        /// </summary>
        [Required]
        public Guid FromAddressId { get; set; }

        /// <summary>
        /// Id of purchase to address
        /// </summary>
        [Required]
        public Guid ToAddressId { get; set; }
    }
}
