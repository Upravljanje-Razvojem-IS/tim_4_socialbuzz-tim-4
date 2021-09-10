using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models.PASType;

namespace PASMicroservice.Profiles
{
    public class PASTypeProfile : Profile
    {
        public PASTypeProfile()
        {
            CreateMap<PASType, PASTypeDto>();
            CreateMap<PASTypeCreationDto, PASType>();
            CreateMap<PASTypeUpdateDto, PASType>();
        }
    }
}
