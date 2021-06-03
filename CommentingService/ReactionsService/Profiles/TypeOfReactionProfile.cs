using AutoMapper;
using ReactionsService.Models;
using ReactionsService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Profiles
{
    public class TypeOfReactionProfile : Profile
    {
        public TypeOfReactionProfile()
        {
            CreateMap<TypeOfReactionModifyingDto, TypeOfReaction>();

            CreateMap<TypeOfReactionCreationDto, TypeOfReaction>();

            CreateMap<TypeOfReaction, TypeOfReactionDto>();

            CreateMap<TypeOfReaction, TypeOfReaction>();
        }
    }
}
