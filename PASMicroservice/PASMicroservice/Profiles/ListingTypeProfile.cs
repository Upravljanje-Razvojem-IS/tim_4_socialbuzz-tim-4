using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models.ListingType;

namespace PASMicroservice.Profiles
{
    public class ListingTypeProfile : Profile
    {
        public ListingTypeProfile()
        {
            CreateMap<ListingType, ListingTypeDto>();
            CreateMap<ListingTypeCreationDto, ListingType>();
            CreateMap<ListingTypeUpdateDto, ListingType>();
        }
    }
}
