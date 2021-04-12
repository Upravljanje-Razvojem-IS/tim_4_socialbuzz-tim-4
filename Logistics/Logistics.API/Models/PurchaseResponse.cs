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

        public Guid DistancePriceId { get; set; }

        public Guid FromAddressId { get; set; }

        public Guid ToAddressId { get; set; }
        public int Pieces { get; set; }
        public double TotalWeight { get; set; }
        public double TotalPriceWithWeightAndDistance { get; set; }
    }
}
