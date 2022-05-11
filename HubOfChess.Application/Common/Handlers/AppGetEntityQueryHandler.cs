using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.Common.Handlers
{
    public class AppGetEntityQueryHandler : 
        IGetEntityQueryHandler<User>,
        IGetEntityQueryHandler<Chat>
    {

        private readonly IAppDbContext dbContext;

        public AppGetEntityQueryHandler(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        async Task<User> IGetEntityQueryHandler<User>.GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(User), id);
            return user;
        }
        async Task<Chat> IGetEntityQueryHandler<Chat>.GetEntityByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var chat = await dbContext.Chats
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (chat == null)
                throw new NotFoundException(nameof(Chat), id);
            return chat;
        }
    }
}
