using Logistics.Core.Entities;
using System;

namespace Logistics.API.Models.AddressModels
{
    public class AddressDetails
    {
        /// <summary>
        /// Id of AddressDetails
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
        /// <summary>
        /// City Body
        /// </summary>
        public City City { get; set; }
    }
}
