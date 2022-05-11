using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;

        public CreatePostCommandHandler(IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Author = user,
                Title = request.Title,
                Text = request.Text,
                Date = DateTime.Now
            };

            await dbContext.Posts.AddAsync(post, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}
