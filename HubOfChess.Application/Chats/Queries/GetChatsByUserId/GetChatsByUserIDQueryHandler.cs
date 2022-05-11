using MediatR;
using AutoMapper;
using HubOfChess.Domain;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;

namespace HubOfChess.Application.Chats.Queries.GetChatsByUserId
{
    public class GetChatsByUserIdQueryHandler : IRequestHandler<GetChatsByUserIdQuery, IEnumerable<ChatVM>>
    {
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IMapper mapper;

        public GetChatsByUserIdQueryHandler(IGetEntityQueryHandler<User> getUserHandler, IMapper mapper)
        {
            this.getUserHandler = getUserHandler;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ChatVM>> Handle(GetChatsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);
            return mapper.Map<IEnumerable<ChatVM>>(user.Chats);
        }
    }
}
