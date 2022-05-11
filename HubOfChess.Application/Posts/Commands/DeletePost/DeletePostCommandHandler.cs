using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Post> getPostHandler;

        public DeletePostCommandHandler(IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Post> getPostHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getPostHandler = getPostHandler;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);
            var post = await getPostHandler
                .GetEntityByIdAsync(request.PostId, cancellationToken);

            if (post.Author != user)
                throw new NoPermissionException(
                    nameof(User), request.UserId,
                    nameof(Post), request.PostId);

            dbContext.Posts.Remove(post);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
