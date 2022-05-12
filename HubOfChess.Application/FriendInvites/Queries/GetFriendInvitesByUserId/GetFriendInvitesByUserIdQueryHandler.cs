using MediatR;
using HubOfChess.Application.Interfaces;
using HubOfChess.Domain;
using AutoMapper;
using HubOfChess.Application.ViewModels;

namespace HubOfChess.Application.FriendInvites.Queries.GetFriendInvitesByUserId
{
    public class GetFriendInvitesByUserIdQueryHandler : IRequestHandler<GetFriendInvitesByUserIdQuery, IEnumerable<FriendInviteVM>>
    {
        private readonly IGetEntityQueryHandler<User> getUserHandler;
        private readonly IMapper mapper;

        public GetFriendInvitesByUserIdQueryHandler(IGetEntityQueryHandler<User> getUserHandler, IMapper mapper)
        {
            this.getUserHandler = getUserHandler;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FriendInviteVM>> Handle(GetFriendInvitesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);

            return mapper.Map<IEnumerable<FriendInviteVM>>(user.FriendInvites);
        }
    }
}
