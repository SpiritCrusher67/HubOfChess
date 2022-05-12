using AutoMapper;
using HubOfChess.Application.Common.Mappings;
using HubOfChess.Domain;

namespace HubOfChess.Application.ViewModels
{
    public class FriendInviteVM : IMapWith<FriendInvite>
    {
        public Guid InvitedUserId { get; set; }
        public string InvitedUserFullName { get; set; } = null!;
        public Guid SenderUserId { get; set; }
        public string SenderUserFullName { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? InviteMessage { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FriendInvite, FriendInviteVM>()
                .ForMember(vm => vm.InvitedUserId,
                    opt => opt.MapFrom(i => i.InvitedUserId))
                .ForMember(vm => vm.SenderUserId,
                    opt => opt.MapFrom(i => i.SenderUserId))
                .ForMember(vm => vm.Date,
                    opt => opt.MapFrom(i => i.Date))
                .ForMember(vm => vm.InviteMessage,
                    opt => opt.MapFrom(i => i.InviteMessage))
                .ForMember(vm => vm.InvitedUserFullName,
                    opt => opt.MapFrom(i =>
                        $"{i.InvitedUser.FirstName} {i.InvitedUser.LastName}"))
                .ForMember(vm => vm.SenderUserFullName,
                    opt => opt.MapFrom(i =>
                        $"{i.SenderUser.FirstName} {i.SenderUser.LastName}"));
        }
    }
}
