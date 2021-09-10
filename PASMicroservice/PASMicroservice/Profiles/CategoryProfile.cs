using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models.Category;

namespace PASMicroservice.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreationDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
