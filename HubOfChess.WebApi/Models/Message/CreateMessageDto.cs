using AutoMapper;
using HubOfChess.Application.Messages.Commands.CreateMessage;

namespace HubOfChess.WebApi.Models.Message
{
    public class CreateMessageDto : BaseDtoModel<CreateMessageCommand>
    {
        public Guid ChatId { get; set; }
        public string Text { get; set; } = null!;

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMessageDto, CreateMessageCommand>()
                .ForMember(command => command.UserId,
                    opt => opt.MapFrom(dto => dto.userId))
                .ForMember(command => command.ChatId,
                    opt => opt.MapFrom(dto => dto.ChatId))
                .ForMember(command => command.Text,
                    opt => opt.MapFrom(dto => dto.Text));
        }
    }
}
