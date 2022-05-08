using AutoMapper;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserVM>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserVM> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);

            return _mapper.Map<UserVM>(user);
        }
    }
}
