using Logistics.Core.Entities;
using System;

namespace Logistics.Core.BusinessLogic
{
    public static class CalculateDistance
    {
        public static double Calculate(City from, City to)
        {
            double lat1 = from.Latitude;
            double lon1 = from.Longitude;

            double lat2 = to.Latitude;
            double lon2 = to.Longitude;

            double R = 6371e3; // metres
            double φ1 = lat1 * Math.PI / 180;
            double φ2 = lat2 * Math.PI / 180;
            double Δφ = (lat2 - lat1) * Math.PI / 180;
            double Δλ = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                      Math.Cos(φ1) * Math.Cos(φ2) *
                      Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = R * c; // in metres

            return d/1000;
        }
    }
}
