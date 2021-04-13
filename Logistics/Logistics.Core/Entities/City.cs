using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Entities
{
    /// <summary>
    /// Represent City
    /// </summary>
    public class City
    {
        /// <summary>
        /// Id of city
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// City name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// City Postal Code
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// City latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// City longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}
