using MediatR;

namespace HubOfChess.Application.PostComments.Commands.CreatePostComment
{
    public record CreatePostCommentCommand(Guid UserId, Guid PostId, string Text) : IRequest<Guid>;
}
