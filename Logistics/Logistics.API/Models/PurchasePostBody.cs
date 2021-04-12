using Logistics.Core.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    public class PurchasePostBody
    {
        public Guid ItemId { get; set; }

        public int Pieces { get; set; }

        public Guid FromAddressId { get; set; }

        public Guid ToAddressId { get; set; }
    }
}
