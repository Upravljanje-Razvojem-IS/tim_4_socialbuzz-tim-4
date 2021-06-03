using System;
using System.ComponentModel.DataAnnotations;

namespace Logistics.Core.Entities
{
    /// <summary>
    /// Represent WeightRange
    /// </summary>
    public class WeightRange
    {
        /// <summary>
        /// Id of WeightRange
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// MinimalWeight for coefficient
        /// </summary>
        [Range(0, float.MaxValue, ErrorMessage = "Minimal Weight must be greater then 0")]
        public float MinimalWeight { get; set; }
        /// <summary>
        /// MaximalWeight for coefficient
        /// </summary>
        [Range(0, float.MaxValue, ErrorMessage = "Minimal Weight must be greater then 0")]
        public float MaximalWeight { get; set; }
        /// <summary>
        /// Coefficient for calculating price
        /// </summary>
        public double PriceCoefficient { get; set; }
    }
}
