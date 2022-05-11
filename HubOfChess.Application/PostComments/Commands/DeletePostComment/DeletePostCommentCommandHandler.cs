using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.PostComments.Commands.DeletePostComment
{
    public class DeletePostCommentCommandHandler : IRequestHandler<DeletePostCommentCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<PostComment> getCommentHandler;

        public DeletePostCommentCommandHandler(IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<PostComment> getCommentHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getCommentHandler = getCommentHandler;
        }

        public async Task<Unit> Handle(DeletePostCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            var comment = await getCommentHandler
                .GetEntityByIdAsync(request.CommentId, cancellationToken);

            if (comment.User != user)
                throw new NoPermissionException(
                    nameof(User), user.UserId, 
                    nameof(PostComment), comment.Id);

            dbContext.PostComments.Remove(comment);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
