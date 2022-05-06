using HubOfChess.Domain;
using HubOfChess.Application.Interfaces;
using MediatR;

namespace HubOfChess.Application.Chats.Commands.CreateChat
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Guid>
    {
        private readonly IAppDbContext _dbContext;

        public CreateChatCommandHandler(IAppDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = new Chat
            {
                Id = Guid.NewGuid(),
                Name = request.ChatName,
                Owner = request.ChatOwner
            };

            await _dbContext.Chats.AddAsync(chat,cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return chat.Id;
        }
    }
}
