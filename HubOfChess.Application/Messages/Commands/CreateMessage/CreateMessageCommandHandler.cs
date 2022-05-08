using MediatR;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Domain;

namespace HubOfChess.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;
        public CreateMessageCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await _dbContext.Chats
                .FirstOrDefaultAsync(c => c.Id == request.ChatId, cancellationToken);
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId,cancellationToken);

            if (chat == null)
                throw new NotFoundException(nameof(Chat), request.ChatId);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
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

            await _dbContext.Messages.AddAsync(message, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return message.Id;
        }
    }
}
