using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models;
using PASMicroservice.Models.ProductsAndServices;

namespace PASMicroservice.Profiles
{
    public class ProductsAndServicesProfile : Profile
    {
        public ProductsAndServicesProfile()
        {
            CreateMap<ProductsAndServices, ProductsAndServicesDto>();
            CreateMap<ProductsAndServicesCreationDto, ProductsAndServices>();
            CreateMap<ProductsAndServicesUpdateDto, ProductsAndServices>();
        }
    }
}
