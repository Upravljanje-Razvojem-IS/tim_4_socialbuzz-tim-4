using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    public class DistancePriceResponse
    {
        public Guid Id { get; set; }
        public int MinimalDistance { get; set; }
        public int MaximalDistance { get; set; }
        public float Price { get; set; }
    }
}
