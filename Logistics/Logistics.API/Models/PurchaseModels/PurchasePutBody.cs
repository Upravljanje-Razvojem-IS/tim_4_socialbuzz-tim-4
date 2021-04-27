using Logistics.Core.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    /// <summary>
    /// Purchase Put model
    /// </summary>
    public class PurchasePutBody
    {
        /// <summary>
        /// Id of purchase item
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Number of item pieces
        /// </summary>
        public int Pieces { get; set; }

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
