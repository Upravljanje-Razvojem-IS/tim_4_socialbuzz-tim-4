using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.BusinessLogic
{
    public static class CalculatePrice
    {
        public static double Calculate(int pieces, double price, double distancePrice, double weightcoefficient)
        {
            return (pieces * price) + (distancePrice * weightcoefficient);
        }
    }
}
