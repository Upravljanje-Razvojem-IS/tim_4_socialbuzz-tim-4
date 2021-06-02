using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.API.Models.WeightRangeModels
{
    /// <summary>
    /// WeightRange put model
    /// </summary>
    public class WeightRangePutBody
    {
        /// <summary>
        /// MinimalWeight for price coefficient
        /// </summary>
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Minimal Weight must be greater then 0")]
        public float MinimalWeight { get; set; }
        /// <summary>
        /// MaximalWeight for price coefficient
        /// </summary>
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Minimal Weight must be greater then 0")]
        public float MaximalWeight { get; set; }
        /// <summary>
        /// coefficient for calculating price
        /// </summary>
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price Coefficient must be greater then 0")]
        public double PriceCoefficient { get; set; }
    }
}
