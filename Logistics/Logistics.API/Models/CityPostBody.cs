using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    /// <summary>
    /// City post model
    /// </summary>
    public class CityPostBody
    {
        /// <summary>
        /// City Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// City PostalCode
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// City Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// City Longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}
