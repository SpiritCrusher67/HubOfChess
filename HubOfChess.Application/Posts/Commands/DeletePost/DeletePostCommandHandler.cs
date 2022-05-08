using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IAppDbContext _dbContext;

        public DeletePostCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
            var post = await _dbContext.Posts
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
            if (post == null)
                throw new NotFoundException(nameof(Post), request.PostId);
            if (post.Author != user)
                throw new NoPermissionException(
                    nameof(User), request.UserId,
                    nameof(Post), request.PostId);

            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
