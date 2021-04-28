using AutoMapper;
using Logistics.API.Models.WeightRangeModels;
using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
