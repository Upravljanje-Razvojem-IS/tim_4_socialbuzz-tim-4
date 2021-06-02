using System;


namespace Logistics.API.Models.AddressModels
{
    /// <summary>
    /// Address overview model
    /// </summary>
    public class AddressOverview
    {
        /// <summary>
        /// Id of AddressOverview
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Street in address
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Address number
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Id of Address City
        /// </summary>
        public Guid CityId { get; set; }
    }
}
