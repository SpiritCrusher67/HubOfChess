using AutoMapper;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.Messages.Queries.GetMessagesByChatId
{
    public class GetMessagesByChatIdQueryHandler : IRequestHandler<GetMessagesByChatIdQuery, IEnumerable<MessageVM>>
    {
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;
        private readonly IMapper mapper;

        public GetMessagesByChatIdQueryHandler(IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Chat> getChatHandler, IMapper mapper)
        {
            this.getUserHandler = getUserHandler;
            this.getChatHandler = getChatHandler;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MessageVM>> Handle(GetMessagesByChatIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            if (!chat.Users.Contains(user))
                throw new NoPermissionException(
                    nameof(User), user.UserId, 
                    nameof(Chat), chat.Id);

            var messages = chat.Messages
                .OrderByDescending(m => m.Date)
                .Skip((request.Page - 1) * request.PageLimit )
                .Take(request.PageLimit);

            return mapper.Map<IEnumerable<MessageVM>>(messages);
        }
    }
}
