using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    public class WeightRangePostBody
    {
        public Guid Id { get; set; }
        public float MinimalWeight { get; set; }
        public float MaximalWeight { get; set; }
        public double PriceCoefficient { get; set; }
    }
}
