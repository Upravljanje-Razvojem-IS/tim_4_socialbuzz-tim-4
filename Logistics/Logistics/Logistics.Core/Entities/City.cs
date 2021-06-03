using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// City name
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// City Postal Code
        /// </summary>dd
        [Required]
        public string PostalCode { get; set; }
        /// <summary>
        /// City latitude
        /// </summary>
        [Required]
        public double Latitude { get; set; }
        /// <summary>
        /// City longitude
        /// </summary>
        [Required]
        public double Longitude { get; set; }
    }
}
