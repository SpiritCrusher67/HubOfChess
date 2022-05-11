using AutoMapper;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.PostComments.Queries.GetPostCommentById
{
    public class GetPostCommentByIdQueryHandler : IRequestHandler<GetPostCommentByIdQuery, PostCommentVM>
    {
        private readonly IGetEntityQueryHandler<PostComment> getCommentHandler;
        private readonly IMapper mapper;

        public GetPostCommentByIdQueryHandler(IGetEntityQueryHandler<PostComment> getCommentHandler, IMapper mapper)
        {
            this.getCommentHandler = getCommentHandler;
            this.mapper = mapper;
        }
        public async Task<PostCommentVM> Handle(GetPostCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await getCommentHandler
                .GetEntityByIdAsync(request.CommentId, cancellationToken);
            return mapper.Map<PostCommentVM>(comment);
        }
    }
}
