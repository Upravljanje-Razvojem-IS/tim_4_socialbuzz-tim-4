using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    public class WeightRangeResponse
    {
        public Guid Id { get; set; }
        public float MinimalWeight { get; set; }
        public float MaximalWeight { get; set; }
    }
}
