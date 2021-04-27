using AutoMapper;
using Logistics.API.Models.CityModels;
using Logistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.MapperProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityOverview>();
        }
    }
}
