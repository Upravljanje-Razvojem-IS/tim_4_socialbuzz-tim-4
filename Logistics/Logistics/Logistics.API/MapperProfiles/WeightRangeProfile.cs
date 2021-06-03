using AutoMapper;
using Logistics.API.Models.WeightRangeModels;
using Logistics.Core.Entities;


namespace Logistics.API.MapperProfiles
{
    public class WeightRangeProfile : Profile
    {
        public WeightRangeProfile()
        {
            CreateMap<WeightRange, WeightRangeOverview>();
            CreateMap<WeightRange, WeightRangeDetails>();
            CreateMap<WeightRange, WeightRangeConfirmation>();
        }
    }
}
