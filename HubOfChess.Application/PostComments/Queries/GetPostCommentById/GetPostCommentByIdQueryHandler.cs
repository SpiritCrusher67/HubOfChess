using AutoMapper;
using HubOfChess.Application.Common.Exceptions;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HubOfChess.Application.PostComments.Queries.GetPostCommentById
{
    public class GetPostCommentByIdQueryHandler : IRequestHandler<GetPostCommentByIdQuery, PostCommentVM>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostCommentByIdQueryHandler(IAppDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PostCommentVM> Handle(GetPostCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _dbContext.PostComments
                .FirstOrDefaultAsync(c => c.Id == request.CommentId);
            if (comment == null)
                throw new NotFoundException(nameof(PostComment), request.CommentId);

            return _mapper.Map<PostCommentVM>(comment);
        }
    }
}
