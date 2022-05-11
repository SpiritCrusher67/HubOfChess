using AutoMapper;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserVM>
    {
        private readonly IMapper mapper;
        private readonly IGetEntityQueryHandler<User> getUserHandler;

        public GetUserByIdQueryHandler(IGetEntityQueryHandler<User> getUserHandler, IMapper mapper)
        {
            this.getUserHandler = getUserHandler;
            this.mapper = mapper;
        }

        public async Task<UserVM> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await getUserHandler
                .GetEntityByIdAsync(request.UserId, cancellationToken);
            return mapper.Map<UserVM>(user);
        }
    }
}
