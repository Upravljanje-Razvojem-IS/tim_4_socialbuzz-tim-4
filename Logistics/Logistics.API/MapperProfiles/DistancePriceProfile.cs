using AutoMapper;
using Logistics.API.Models.DistancePriceModels;
using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.MapperProfiles
{
    public class DistancePriceProfile : Profile
    {
        public DistancePriceProfile()
        {
            CreateMap<DistancePrice, DistancePriceOverview>();
        }
    }
}
