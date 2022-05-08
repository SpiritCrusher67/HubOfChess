using MediatR;

namespace HubOfChess.Application.PostLikes.Commands.DeletePostLike
{
    public record DeletePostLikeCommand(Guid UserId, Guid PostId) : IRequest;
}
