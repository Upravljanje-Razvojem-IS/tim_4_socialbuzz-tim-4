using AutoMapper;
using ChatService.DTOs;
using ChatService.Entities;

namespace ChatService.MapperProfiles
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<Message, MessageConfirmationDto>();
            CreateMap<Message, MessageReadDto>();
        }
    }
}
