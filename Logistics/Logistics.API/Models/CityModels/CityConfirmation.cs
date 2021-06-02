using System;

namespace Logistics.API.Models.CityModels
{
    /// <summary>
    /// CityConfirmation model
    /// </summary>
    public class CityConfirmation
    {
        /// <summary>
        /// City confirmation Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// City Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// City PostalCode
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// City Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// City Longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}
