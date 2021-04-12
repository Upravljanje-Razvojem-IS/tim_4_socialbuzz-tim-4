using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Entities
{
    public class DistancePrice
    {
        public Guid Id { get; set; }
        public int MinimalDistance { get; set; }
        public int MaximalDistance { get; set; }
        public double Price { get; set; }
    }
}
