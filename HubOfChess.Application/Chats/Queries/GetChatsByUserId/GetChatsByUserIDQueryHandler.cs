using HubOfChess.Domain;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.Chats.Queries.GetChatsByUserId
{
    public class GetChatsByUserIdQueryHandler : IRequestHandler<GetChatsByUserIdQuery, IEnumerable<Chat>>
    {
        private readonly IAppDbContext _dbContext;

        public GetChatsByUserIdQueryHandler(IAppDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<IEnumerable<Chat>> Handle(GetChatsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);

            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);

            return user.Chats;
        }
    }
}
