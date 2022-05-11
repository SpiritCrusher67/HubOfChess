using MediatR;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Domain;
using HubOfChess.Application.ViewModels;
using AutoMapper;

namespace HubOfChess.Application.Chats.Queries.GetChatById
{
    public class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, ChatVM>
    {
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;
        private readonly IMapper mapper;

        public GetChatByIdQueryHandler(IGetEntityQueryHandler<User> getUserHandler, IGetEntityQueryHandler<Chat> getChatHandler, IMapper mapper)
        {
            this.getUserHandler = getUserHandler;
            this.getChatHandler = getChatHandler;
            this.mapper = mapper;
        }

        public async Task<ChatVM> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            if (!chat.Users.Contains(user))
                throw new NoPermissionException(
                    nameof(User),user.UserId, 
                    nameof(Chat), chat.Id);

            return mapper.Map<ChatVM>(chat);
        }
    }
}
