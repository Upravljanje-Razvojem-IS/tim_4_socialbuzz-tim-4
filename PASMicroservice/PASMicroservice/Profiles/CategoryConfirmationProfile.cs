using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models.Category;

namespace PASMicroservice.Profiles
{
    public class CategoryConfirmationProfile : Profile
    {
        public CategoryConfirmationProfile()
        {
            CreateMap<CategoryConfirmation, CategoryConfirmationDto>();
        }
    }
}
