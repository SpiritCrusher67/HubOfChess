using MediatR;

namespace HubOfChess.Application.PostLikes.Commands
{
    public record CreatePostLikeCommand(Guid UserId, Guid PostId) : IRequest;
}
