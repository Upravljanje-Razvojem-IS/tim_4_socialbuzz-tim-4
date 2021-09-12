using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models;
using PASMicroservice.Models.Listing;

namespace PASMicroservice.Profiles
{
    public class ListingProfile : Profile
    {
        public ListingProfile()
        {
            CreateMap<Listing, ListingDto>();
            CreateMap<ListingCreationDto, Listing>();
            CreateMap<ListingUpdateDto, Listing>();
        }
    }
}
