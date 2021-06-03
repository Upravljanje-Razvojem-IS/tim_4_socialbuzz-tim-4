using System;

namespace Logistics.Core.Mock
{
    /// <summary>
    /// Mock for item
    /// </summary>
    public class ItemMock
    {
        /// <summary>
        /// Item id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Item name
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        /// weight of one 
        /// </summary>
        public float WeightOfOne { get; set; }
        /// <summary>
        /// price of one
        /// </summary>
        public double PriceOfOne { get; set; }
    }
}
