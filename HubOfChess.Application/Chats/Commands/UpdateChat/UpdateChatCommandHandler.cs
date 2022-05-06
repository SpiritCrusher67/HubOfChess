using MediatR;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Domain;

namespace HubOfChess.Application.Chats.Commands.UpdateChat
{
    public class UpdateChatCommandHandler : IRequestHandler<UpdateChatCommand>
    {
        private readonly IAppDbContext _dbContext;

        public UpdateChatCommandHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _dbContext.Chats
                .FirstOrDefaultAsync(c => c.Id == request.ChatId);
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);
            var ownerUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.ChatOwnerId);

            if (chat == null)
                throw new NotFoundException(nameof(Chat), request.ChatId);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
            if (chat.Owner != user)
                throw new NoPermissionException(
                    nameof(User), user.UserId, 
                    nameof(Chat), chat.Id);

            chat.Name = request.ChatName;
            if (chat.Users.Contains(ownerUser))
                chat.Owner = ownerUser;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
