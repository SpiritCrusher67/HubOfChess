using MediatR;

namespace HubOfChess.Application.Messages.Commands.DeleteMessage
{
    public record DeleteMessageCommand(Guid MessageId, Guid UserId) : IRequest;
}
