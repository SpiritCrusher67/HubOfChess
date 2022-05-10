using AutoMapper;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.Messages.Queries.GetMessagesByChatId
{
    public class GetMessagesByChatIdQueryHandler : IRequestHandler<GetMessagesByChatIdQuery, IEnumerable<MessageVM>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMessagesByChatIdQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<IEnumerable<MessageVM>> Handle(GetMessagesByChatIdQuery request, CancellationToken cancellationToken)
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
                    nameof(User), user.UserId, 
                    nameof(Chat), chat.Id);

            var messages = chat.Messages
                .OrderByDescending(m => m.Date)
                .Skip((request.Page - 1) * request.PageLimit )
                .Take(request.PageLimit);

            return _mapper.Map<IEnumerable<MessageVM>>(messages);
        }
    }
}
