using AutoMapper;
using HubOfChess.Application.FriendInvites.Commands.CreateFriendInvite;

namespace HubOfChess.WebApi.Models.User
{
    public class CreateFriendInviteDto : BaseDtoModel<CreateFriendInviteCommand>
    {
        public Guid InvitedUserId { get; set; }
        public string? Message { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFriendInviteDto, CreateFriendInviteCommand>()
                .ForMember(command => command.SenderUserId,
                    opt => opt.MapFrom(dto => dto.userId))
                .ForMember(command => command.InvitedUserId,
                    opt => opt.MapFrom(dto => dto.InvitedUserId))
                .ForMember(command => command.InviteMessage,
                    opt => opt.MapFrom(dto => dto.Message));
        }
    }
}
