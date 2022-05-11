using AutoMapper;
using HubOfChess.Application.Common.Mappings;
using HubOfChess.Domain;

namespace HubOfChess.Application.ViewModels
{
    public class ChatVM : IMapWith<Chat>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerFullName { get; set; } = null!;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Chat, ChatVM>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(c => c.Id))
                .ForMember(vm => vm.Name,
                    opt => opt.MapFrom(c => c.Name))
                .ForMember(vm => vm.OwnerId,
                    opt => opt.MapFrom(c => c.Owner.UserId))
                .ForMember(vm => vm.OwnerFullName,
                    opt => opt.MapFrom(c => $"{c.Owner.FirstName} {c.Owner.LastName}"));
        }
    }
}
