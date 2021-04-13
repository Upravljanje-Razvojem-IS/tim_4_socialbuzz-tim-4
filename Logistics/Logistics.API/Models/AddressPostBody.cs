using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    /// <summary>
    /// Address model for post
    /// </summary>
    public class AddressPostBody
    {
        /// <summary>
        /// Street in address
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Address number
        /// </summary>
        public int Number { get; set; }
    }
}
