using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models.AddressModels
{
    /// <summary>
    /// Address model for put
    /// </summary>
    public class AddressPutBody
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
