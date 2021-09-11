using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Entities
{
    public class ListingType
    {
        public int ListingTypeId { get; set; }
        public string Name { get; set; }

        public List<Listing> Listings { get; set; }
    }
}
