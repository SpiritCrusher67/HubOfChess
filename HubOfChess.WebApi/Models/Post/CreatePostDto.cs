using AutoMapper;
using HubOfChess.Application.Posts.Commands.CreatePost;

namespace HubOfChess.WebApi.Models.Post
{
    public class CreatePostDto : BaseDtoModel<CreatePostCommand>
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePostDto, CreatePostCommand>()
                .ForMember(command => command.UserId,
                    opt => opt.MapFrom(dto => dto.userId))
                .ForMember(command => command.Title,
                    opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.Text,
                    opt => opt.MapFrom(dto => dto.Text));
        }
    }
}
