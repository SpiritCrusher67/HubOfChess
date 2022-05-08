using MediatR;

namespace HubOfChess.Application.PostLikes.Commands.CreatePostLike
{
    public record CreatePostLikeCommand(Guid UserId, Guid PostId) : IRequest;
}
