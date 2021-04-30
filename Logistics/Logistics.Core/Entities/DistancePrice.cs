using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.Core.Entities
{
    /// <summary>
    /// Represent DistancePrice
    /// </summary>
    public class DistancePrice
    {
        /// <summary>
        /// If of DistancePrice
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// MinimalDistance for price
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minimal Distance must be greater then 0")]
        public int MinimalDistance { get; set; }
        /// <summary>
        /// MaximalDistance for price
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Maximal Distance must be greater then 1")]
        public int MaximalDistance { get; set; }
        /// <summary>
        /// Price for distance
        /// </summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater then 0")]
        public double Price { get; set; }
    }
}
