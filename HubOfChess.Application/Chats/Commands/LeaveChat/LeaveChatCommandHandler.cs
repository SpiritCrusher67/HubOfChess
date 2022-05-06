using MediatR;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Domain;

namespace HubOfChess.Application.Chats.Commands.LeaveChat
{
    public class LeaveChatCommandHandler : IRequestHandler<LeaveChatCommand, bool>
    {
        private readonly IAppDbContext _dbContext;

        public LeaveChatCommandHandler(IAppDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<bool> Handle(LeaveChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _dbContext.Chats
                .FirstOrDefaultAsync(c => c.Id == request.ChatId, cancellationToken);
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

            if (chat == null)
                throw new NotFoundException(nameof(Chat), request.ChatId);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
            if (!chat.Users.Contains(user))
                throw new NoPermissionException(
                    nameof(User), user.UserId, nameof(Chat), chat.Id);

            if (!chat.Users.Remove(user))
                return false;

            if (chat.Owner == user)
                chat.Owner = chat.Users.FirstOrDefault(u => u.UserId != user.UserId);

            if (chat.Users.Count == 0)
                _dbContext.Chats.Remove(chat);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
