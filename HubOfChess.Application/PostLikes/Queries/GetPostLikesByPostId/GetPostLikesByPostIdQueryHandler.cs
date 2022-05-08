using AutoMapper;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.PostLikes.Queries.GetPostLikesByPostId
{
    public class GetPostLikesByPostIdQueryHandler : IRequestHandler<GetPostLikesByPostIdQuery, IEnumerable<PostLikeVM>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostLikesByPostIdQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<IEnumerable<PostLikeVM>> Handle(GetPostLikesByPostIdQuery request, CancellationToken cancellationToken)
        {
            var likesList = await _dbContext.PostLikes
                .Where(l => l.PostId == request.PostId)
                .ToListAsync();

            return _mapper.Map <IEnumerable<PostLikeVM>>(likesList);
        }
    }
}
