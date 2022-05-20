using AutoMapper;
using HubOfChess.Application.Chats.Commands.CreateChat;

namespace HubOfChess.WebApi.Models.Chat
{
    public class CreateChatDto : BaseDtoModel<CreateChatCommand>
    {
        public string? ChatName { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<CreateChatDto, CreateChatCommand>()
                .ForMember(command => command.ChatOwnerUserId,
                    opt => opt.MapFrom(dto => dto.userId))
                .ForMember(command => command.ChatName,
                    opt => opt.MapFrom(dto => dto.ChatName));
        }
    }
}
