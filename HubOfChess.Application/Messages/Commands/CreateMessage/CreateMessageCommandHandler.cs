using MediatR;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;

namespace HubOfChess.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Guid>
    {
        private readonly IAppDbContext dbContext;
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;

        public CreateMessageCommandHandler(IAppDbContext dbContext, 
            IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Chat> getChatHandler)
        {
            this.dbContext = dbContext;
            this.getUserHandler = getUserHandler;
            this.getChatHandler = getChatHandler;
        }

        public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            if (!chat.Users.Contains(user))
                throw new NoPermissionException(
                    nameof(User), user.UserId,
                    nameof(Chat), chat.Id);

            var message = new Message
            {
                Id = Guid.NewGuid(),
                Chat = chat,
                Sender = user,
                Text = request.Text,
                Date = DateTime.Now,
            };

            await dbContext.Messages.AddAsync(message, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return message.Id;
        }
    }
}
