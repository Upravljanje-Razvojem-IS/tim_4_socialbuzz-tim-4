using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models.AddressModels
{
    /// <summary>
    /// Address confirmation model
    /// </summary>
    public class AddressConfirmation
    {
        /// <summary>
        /// Id of AddressConfirmation
        /// </summary>
        public Guid Id { get; set; }
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
