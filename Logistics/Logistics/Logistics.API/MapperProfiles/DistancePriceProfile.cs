using AutoMapper;
using Logistics.API.Models.DistancePriceModels;
using Logistics.Core.Entities;

namespace Logistics.API.MapperProfiles
{
    public class DistancePriceProfile : Profile
    {
        public DistancePriceProfile()
        {
            CreateMap<DistancePrice, DistancePriceOverview>();
            CreateMap<DistancePrice, DistancePriceDetails>();
            CreateMap<DistancePrice, DistancePriceConfirmation>();
        }
    }
}
