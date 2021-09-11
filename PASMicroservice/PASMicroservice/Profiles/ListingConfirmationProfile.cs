using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models.Listing;

namespace PASMicroservice.Profiles
{
    public class ListingConfirmationProfile : Profile
    {
        public ListingConfirmationProfile()
        {
            CreateMap<ListingConfirmation, ListingConfirmationDto>();
        }
    }
}
