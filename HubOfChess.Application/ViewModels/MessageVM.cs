using AutoMapper;
using HubOfChess.Application.Common.Mappings;
using HubOfChess.Domain;

namespace HubOfChess.Application.ViewModels
{
    public class MessageVM : IMapWith<Message>
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageVM>()
                .ForMember(vm => vm.Id,
                    opt => opt.MapFrom(msg => msg.Id))
                .ForMember(vm => vm.ChatId,
                    opt => opt.MapFrom(msg => msg.Chat.Id))
                .ForMember(vm => vm.SenderId,
                    opt => opt.MapFrom(msg => msg.Sender.UserId))
                .ForMember(vm => vm.Text,
                    opt => opt.MapFrom(msg => msg.Text))
                .ForMember(vm => vm.Date,
                    opt => opt.MapFrom(msg => msg.Date));
        }
    }
}
