using HubOfChess.Domain;
using HubOfChess.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Application.Common.Exceptions;

namespace HubOfChess.Application.Chats.Commands.CreateChat
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;

        public CreateChatCommandHandler(IAppDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.ChatOwner, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(User), request.ChatOwner);

            var chat = new Chat
            {
                Id = Guid.NewGuid(),
                Name = request.ChatName,
                Owner = user
            };

            await _dbContext.Chats.AddAsync(chat,cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return chat.Id;
        }
    }
}
