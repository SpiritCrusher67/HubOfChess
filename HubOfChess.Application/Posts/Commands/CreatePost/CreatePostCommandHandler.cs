using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;

        public CreatePostCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);

            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Author = user,
                Title = request.Title,
                Text = request.Text,
                Date = DateTime.Now
            };

            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}
