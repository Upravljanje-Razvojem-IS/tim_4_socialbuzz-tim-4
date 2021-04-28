using AutoMapper;
using Logistics.API.Models.AddressModels;
using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.MapperProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressOverview>();
            CreateMap<Address, AddressDetails>();
            CreateMap<Address, AddressConfirmation>();
        }
    }
}
