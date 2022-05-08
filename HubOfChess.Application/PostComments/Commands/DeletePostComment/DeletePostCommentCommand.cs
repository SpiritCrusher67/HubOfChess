using MediatR;

namespace HubOfChess.Application.PostComments.Commands.DeletePostComment
{
    public record DeletePostCommentCommand(Guid UserId, Guid CommentId) : IRequest;
}
