using MediatR;
using HubOfChess.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Domain;
using HubOfChess.Application.Common.Exceptions;

namespace HubOfChess.Application.Chats.Commands.DeleteChat
{
    public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand>
    {
        private readonly IAppDbContext _dbContext;

        public DeleteChatCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _dbContext.Chats
                .FirstOrDefaultAsync(c => c.Id == request.ChatId);

            if (chat == null || chat.Owner?.UserId != request.UserId)
                throw new NotFoundException(nameof(Chat), request.ChatId);

            _dbContext.Chats.Remove(chat);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
