namespace CommentingService.Profiles
{
    using AutoMapper;
    using CommentingService.Entities;
    using CommentingService.Models;
    using CommentingService.Models.Dto;

    public class CommentsProfile : Profile
    {
       //Mapiranja kod kreiranja i modifikacije komentara --> DTO & Entity
        public CommentsProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentCreationDto, Comment>();
            CreateMap<CommentModifyingDto, Comment>();
            CreateMap<Comment, Comment>();
        }
    }
}
