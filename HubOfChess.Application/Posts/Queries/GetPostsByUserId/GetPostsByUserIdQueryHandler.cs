using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HubOfChess.Domain;
using AutoMapper;

namespace HubOfChess.Application.Posts.Queries.GetPostsByUserId
{
    public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, IEnumerable<PostVM>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetPostsByUserIdQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<IEnumerable<PostVM>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);

            var posts = user.Posts
                .OrderByDescending(p => p.Date)
                .Skip((request.Page - 1) * request.PageLimit)
                .Take(request.PageLimit);

            return _mapper.Map<IEnumerable<PostVM>>(posts);
        }
    }
}
