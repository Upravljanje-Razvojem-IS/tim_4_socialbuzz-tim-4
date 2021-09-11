using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models.ListingType;

namespace PASMicroservice.Profiles
{
    public class ListingTypeConfirmationProfile : Profile
    {
        public ListingTypeConfirmationProfile()
        {
            CreateMap<ListingTypeConfirmation, ListingTypeConfirmationDto>();
        }
    }
}
