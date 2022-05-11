using AutoMapper;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.ChatInvites.Queries.GetChatInvitesByUserId
{
    public class GetChatInvitesByUserIdQueryHandler : IRequestHandler<GetChatInvitesByUserIdQuery, IEnumerable<ChatInviteVM>>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetChatInvitesByUserIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ChatInviteVM>> Handle(GetChatInvitesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .Include(u => u.ChatInvites)
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);

            return mapper.Map<IEnumerable<ChatInviteVM>>(user.ChatInvites);
        }
    }
}
