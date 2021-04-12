using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Mock
{
    public class ItemMock
    {
        public Guid Id { get; set; }
        public string Item { get; set; }
        public float WeightOfOne { get; set; }
        public double PriceOfOne { get; set; }
    }
}
