using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Core.Entities
{
    /// <summary>
    /// Represent addess
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Id of address
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of street
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Address number
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// City id of address
        /// </summary>
        public Guid CityId { get; set; }
        /// <summary>
        /// City of address
        /// </summary>
        public City City { get; set; }
    }
}
