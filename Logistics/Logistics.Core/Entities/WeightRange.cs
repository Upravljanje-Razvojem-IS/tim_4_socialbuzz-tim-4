using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Entities
{
    public class WeightRange
    {
        public Guid Id { get; set; }
        public float MinimalWeight { get; set; }
        public float MaximalWeight { get; set; }
        public float PriceCoefficient { get; set; }
    }
}
