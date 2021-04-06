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
        public string Item { get; set; }
        public float Weight { get; set; }
        public int Pieces { get; set; }

        public Guid WeightRangeId { get; set; }
        public WeightRange WeightRange { get; set; }

        public Guid DeliveryServiceId { get; set; }
        public DeliveryService DeliveryService { get; set; }

        public Guid FromAddressId { get; set; }
        public Address FromAddess { get; set; }

        public Guid ToAddressId { get; set; }
        public Guid ToAddress { get; set; }

    }
}
