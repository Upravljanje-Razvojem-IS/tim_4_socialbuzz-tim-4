using AutoMapper;
using ReactionsService.Models;
using ReactionsService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Profiles
{
    public class ReactionProfile : Profile
    {
        public ReactionProfile()
        {
            CreateMap<ReactionModifyingDto, Reaction>();

            CreateMap<ReactionCreationDto, Reaction>();

            CreateMap<Reaction, ReactionsDto>();

            CreateMap<Reaction, Reaction>();
        }
    }
}
