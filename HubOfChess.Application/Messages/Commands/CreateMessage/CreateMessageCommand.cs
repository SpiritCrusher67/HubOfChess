using MediatR;

namespace HubOfChess.Application.Messages.Commands.CreateMessage
{
    public record CreateMessageCommand(Guid ChatId, Guid UserId, string Text) : IRequest<Guid>;
}
