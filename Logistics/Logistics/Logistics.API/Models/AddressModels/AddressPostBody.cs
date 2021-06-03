using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.API.Models.AddressModels
{
    /// <summary>
    /// Address model for post
    /// </summary>
    public class AddressPostBody
    {
        /// <summary>
        /// Street in address
        /// </summary>
        [Required(ErrorMessage = "Required")]
        public string Street { get; set; }
        /// <summary>
        /// Address number
        /// </summary>
        [Required(ErrorMessage = "Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Address Number must be greater than 0")]
        public int Number { get; set; }
    }
}
