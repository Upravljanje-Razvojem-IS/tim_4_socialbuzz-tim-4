﻿using System.ComponentModel.DataAnnotations;

namespace Logistics.API.Models.CityModels
{
    /// <summary>
    /// City put model
    /// </summary>
    public class CityPutBody
    {
        /// <summary>
        /// City Name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// City PostalCode
        /// </summary>
        [Required]
        public string PostalCode { get; set; }
        /// <summary>
        /// City Latitude
        /// </summary>
        [Required]
        public double Latitude { get; set; }
        /// <summary>
        /// City Longitude
        /// </summary>
        [Required]
        public double Longitude { get; set; }
    }
}
