using HubOfChess.Application.ViewModels;
using HubOfChess.Application.Interfaces;
using MediatR;
using HubOfChess.Domain;
using HubOfChess.Application.Common.Exceptions;
using AutoMapper;

namespace HubOfChess.Application.ChatInvites.Queries.GetChatInvitesByChatId
{
    public class GetChatInvitesByChatIdQueryHandler : IRequestHandler<GetChatInvitesByChatIdQuery, IEnumerable<ChatInviteVM>>
    {
        private readonly IGetEntityQueryHandler<Chat> getChatHandler;
        private readonly IMapper mapper;

        public GetChatInvitesByChatIdQueryHandler(IGetEntityQueryHandler<Chat> getChatHandler, IMapper mapper)
        {
            this.getChatHandler = getChatHandler;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ChatInviteVM>> Handle(GetChatInvitesByChatIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await getChatHandler
                .GetEntityByIdAsync(request.ChatId, cancellationToken);
            if (chat.Owner.UserId != request.UserId)
                throw new NoPermissionException(
                    nameof(User), request.UserId, 
                    nameof(Chat), chat.Id);

            return mapper.Map<IEnumerable<ChatInviteVM>>(chat.Invites);
        }
    }
}
