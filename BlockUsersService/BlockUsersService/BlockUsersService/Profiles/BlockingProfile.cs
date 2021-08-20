using AutoMapper;
using BlockUsersService.Entities;
using BlockUsersService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.Profiles
{
    public class BlockingProfile : Profile
    {
        public BlockingProfile()
        {
            CreateMap<Block, BlockDto>();
            CreateMap<BlockCreationDto, Block>();
            CreateMap<BlockModifyDto, Block>();
        }
    }
}
