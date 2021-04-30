using Logistics.Core.Mock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models.PurchaseModels
{
    /// <summary>
    /// Purchase post model
    /// </summary>
    public class PurchasePostBody
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
