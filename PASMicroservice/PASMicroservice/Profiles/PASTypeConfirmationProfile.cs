using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PASMicroservice.Entities;
using PASMicroservice.Models.PASType;

namespace PASMicroservice.Profiles
{
    public class PASTypeConfirmationProfile : Profile
    {
        public PASTypeConfirmationProfile()
        {
            CreateMap<PASTypeConfirmation, PASTypeConfirmationDto>();
        }
    }
}
