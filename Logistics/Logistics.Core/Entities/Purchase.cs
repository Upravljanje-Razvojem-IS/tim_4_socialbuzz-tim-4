using Logistics.Core.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Entities
{
    public class Purchase
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public int Pieces { get; set; }
        public double TotalWeight { get; set; }
        public double TotalPriceWithWeightAndDistance { get; set; }
        public double Distance { get; set; }

        public Guid WeightRangeId { get; set; }
        public WeightRange WeightRange { get; set; }

        public Guid DistancePriceId { get; set; }
        public DistancePrice DistancePrice { get; set; }

        public Guid FromAddressId { get; set; }
        public Address FromAddess { get; set; }

        public Guid ToAddressId { get; set; }
        public Address ToAddress { get; set; }

    }
}
