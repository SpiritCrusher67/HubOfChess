using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.PostComments.Commands.CreatePostComment
{
    public class CreatePostCommentCommandHandler : IRequestHandler<CreatePostCommentCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;

        public CreatePostCommentCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePostCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts
                .FirstOrDefaultAsync(p => p.Id == request.PostId);
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);

            if (post == null)
                throw new NotFoundException(nameof(Post), request.PostId);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);

            var comment = new PostComment
            {
                Id = Guid.NewGuid(),
                User = user,
                Post = post,
                Text = request.Text,
                Date = DateTime.Now
            };

            await _dbContext.PostComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }
}
