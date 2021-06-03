using System;

namespace Logistics.API.Models.WeightRangeModels
{
    /// <summary>
    /// WEightRange Confirmation model
    /// </summary>
    public class WeightRangeConfirmation
    {
        /// <summary>
        /// weight range id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// MinimalWeight for price coefficient
        /// </summary>
        public float MinimalWeight { get; set; }
        /// <summary>
        /// MaximalWeight for price coefficient
        /// </summary>
        public float MaximalWeight { get; set; }
    }
}
