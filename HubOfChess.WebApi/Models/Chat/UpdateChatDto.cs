using AutoMapper;
using HubOfChess.Application.Chats.Commands.UpdateChat;

namespace HubOfChess.WebApi.Models.Chat
{
    public class UpdateChatDto : BaseDtoModel<UpdateChatCommand>
    {
        public Guid ChatId { get; set; }
        public string? ChatName { get; set; }
        public Guid? NewChatOwnerId { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateChatDto, UpdateChatCommand>()
                .ForMember(command => command.UserId,
                    opt => opt.MapFrom(dto => dto.userId))
                .ForMember(command => command.ChatName,
                    opt => opt.MapFrom(dto => dto.ChatName))
                .ForMember(command => command.ChatOwnerId,
                    opt => opt.MapFrom(dto => dto.NewChatOwnerId));
        }
    }
}
