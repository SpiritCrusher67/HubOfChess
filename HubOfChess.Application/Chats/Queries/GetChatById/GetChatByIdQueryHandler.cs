using MediatR;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Domain;

namespace HubOfChess.Application.Chats.Queries.GetChatById
{
    public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, Chat>
    {
        private readonly IAppDbContext _dbContext;

        public GetChatByIdQueryHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Chat> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
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
                    nameof(User),user.UserId, 
                    nameof(Chat), chat.Id);

            return chat;
        }
    }
}
