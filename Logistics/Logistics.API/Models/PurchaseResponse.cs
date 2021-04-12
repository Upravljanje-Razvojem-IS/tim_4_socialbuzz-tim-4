using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    public class PurchaseResponse
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }

        public Guid WeightRangeId { get; set; }

        public Guid DistanceServiceId { get; set; }

        public Guid FromAddressId { get; set; }

        public Guid ToAddressId { get; set; }
        public int Pieces { get; set; }
        public float TotalWeight { get; set; }
        public float TotalPriceWithWeightAndDistance { get; set; }
    }
}
