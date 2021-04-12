using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    public class AddressResponse
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public Guid CityId { get; set; }
    }
}
