using AutoMapper;
using HubOfChess.Application.PostComments.Commands.CreatePostComment;

namespace HubOfChess.WebApi.Models.Post
{
    public class CreatePostCommentDto : BaseDtoModel<CreatePostCommentCommand>
    {
        public Guid PostId { get; set; }
        public string Text { get; set; } = null!;

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePostCommentDto, CreatePostCommentCommand>()
                .ForMember(command => command.UserId,
                    opt => opt.MapFrom(dto => dto.userId))
                .ForMember(command => command.PostId,
                    opt => opt.MapFrom(dto => dto.PostId))
                .ForMember(command => command.Text,
                    opt => opt.MapFrom(dto => dto.Text));
        }
    }
}
