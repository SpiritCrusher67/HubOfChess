using MediatR;

namespace HubOfChess.Application.Posts.Commands.DeletePost
{
    public record DeletePostCommand(Guid PostId, Guid UserId) : IRequest;
}
