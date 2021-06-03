using AutoMapper;
using Logistics.API.Models.CityModels;
using Logistics.Core.Entities;

namespace Logistics.API.MapperProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityOverview>();
            CreateMap<City, CityDetails>();
            CreateMap<City, CityConfirmation>();
        }
    }
}
