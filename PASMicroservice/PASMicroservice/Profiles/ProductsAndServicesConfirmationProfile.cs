using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models.ProductsAndServices;

namespace PASMicroservice.Profiles
{
    public class ProductsAndServicesConfirmationProfile : Profile
    {
        public ProductsAndServicesConfirmationProfile()
        {
            CreateMap<ProductsAndServicesConfirmation, ProductsAndServicesConfirmationDto>();
        }
    }
}
