using HubOfChess.Domain;
using HubOfChess.Application.Interfaces;
using MediatR;

namespace HubOfChess.Application.Chats.Commands.CreateChat
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Guid>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;

        public CreateChatCommandHandler(IAppDbContext dbContext, IGetEntityQueryHandler<User> getUserHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
        }

        public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var user = await getUserHandler
                .GetEntityByIdAsync(request.ChatOwnerUserId, cancellationToken);

            var chat = new Chat
            {
                Id = Guid.NewGuid(),
                Name = request.ChatName,
                Owner = user
            };

            await dbContext.Chats.AddAsync(chat,cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return chat.Id;
        }
    }
}
