using AutoMapper;
using HubOfChess.Application.Interfaces;
using HubOfChess.Application.ViewModels;
using HubOfChess.Domain;
using MediatR;

namespace HubOfChess.Application.PostComments.Queries.GetPostCommentsByPostId
{
    public class GetPostCommentsByPostIdQueryHandler : IRequestHandler<GetPostCommentsByPostIdQuery, IEnumerable<PostCommentVM>>
    {
        private readonly IGetEntityQueryHandler<Post> getPostHandler;
        private readonly IMapper mapper;

        public GetPostCommentsByPostIdQueryHandler(IGetEntityQueryHandler<Post> getPostHandler, IMapper mapper)
        {
            this.getPostHandler = getPostHandler;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PostCommentVM>> Handle(GetPostCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var post = await getPostHandler
                .GetEntityByIdAsync(request.PostId, cancellationToken);
            return mapper.Map<IEnumerable<PostCommentVM>>(post.Comments);
        }
    }
}
