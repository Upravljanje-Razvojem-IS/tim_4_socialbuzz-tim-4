using AutoMapper;
using Logistics.API.Models.AddressModels;
using Logistics.Core.Entities;

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
