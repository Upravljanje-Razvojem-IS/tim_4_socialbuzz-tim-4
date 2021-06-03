using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of street
        /// </summary>
        [Required]
        public string Street { get; set; }
        /// <summary>
        /// Address number
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number must be greater then 0")]
        public int Number { get; set; }
        /// <summary>
        /// City id of address
        /// </summary>
        [Required]
        [ForeignKey("City")]
        public Guid CityId { get; set; }
        /// <summary>
        /// City of address
        /// </summary>
        public City City { get; set; }
    }
}
