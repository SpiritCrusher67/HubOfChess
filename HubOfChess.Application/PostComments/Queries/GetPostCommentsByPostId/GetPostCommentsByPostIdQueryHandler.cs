using AutoMapper;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.PostComments.Queries.GetPostCommentsByPostId
{
    public class GetPostCommentsByPostIdQueryHandler : IRequestHandler<GetPostCommentsByPostIdQuery, IEnumerable<PostCommentVM>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostCommentsByPostIdQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<IEnumerable<PostCommentVM>> Handle(GetPostCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);
            if (post == null)
                throw new NotFoundException(nameof(Post), request.PostId);

            var postCommentsList = post.Comments
                .Where(c => c.Post.Id == request.PostId)
                .ToList();

            return _mapper.Map<IEnumerable<PostCommentVM>>(postCommentsList);
        }
    }
}
