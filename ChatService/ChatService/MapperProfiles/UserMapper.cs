using AutoMapper;
using ChatService.DTOs;
using ChatService.Entities;

namespace ChatService.MapperProfiles
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserConfirmationDto>();
            CreateMap<User, UserReadDto>();
        }
    }
}
