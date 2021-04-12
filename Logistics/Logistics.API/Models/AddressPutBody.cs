using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Models
{
    public class AddressPutBody
    {
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
