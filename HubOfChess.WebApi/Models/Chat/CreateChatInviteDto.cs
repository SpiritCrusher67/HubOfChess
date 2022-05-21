using AutoMapper;
using HubOfChess.Application.ChatInvites.Commands.CreateChatInvite;

namespace HubOfChess.WebApi.Models.Chat
{
    public class CreateChatInviteDto : BaseDtoModel<CreateChatInviteCommand>
    {
        public Guid ChatId { get; set; }
        public Guid InvitedUserId { get; set; }
        public string? Message { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<CreateChatInviteDto, CreateChatInviteCommand>()
                .ForMember(command => command.SenderUserId,
                    opt => opt.MapFrom(dto => dto.userId))
                .ForMember(command => command.InvitedUserId,
                    opt => opt.MapFrom(dto => dto.InvitedUserId))
                .ForMember(command => command.InviteMessage,
                    opt => opt.MapFrom(dto => dto.Message));
        }
    }
}
