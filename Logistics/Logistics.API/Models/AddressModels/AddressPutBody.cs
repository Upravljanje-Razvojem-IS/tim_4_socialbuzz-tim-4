using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Required")]
        public string Street { get; set; }
        /// <summary>
        /// Address number
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Address Number must be greater then 0")]
        public int Number { get; set; }
    }
}
