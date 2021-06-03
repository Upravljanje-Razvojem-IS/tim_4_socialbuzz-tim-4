using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.API.Models.DistancePriceModels
{
    /// <summary>
    /// DistancePrice put model
    /// </summary>
    public class DistancePricePutBody
    {
        /// <summary>
        /// MinimalDistance for price
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Minimal Distance must be greater then 0")]
        public int MinimalDistance { get; set; }
        /// <summary>
        /// MaximalDistance for price
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Maximal Distance must be greater then 0")]
        public int MaximalDistance { get; set; }
        /// <summary>
        /// Distance price
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater then 0")]
        public double Price { get; set; }
    }
}
