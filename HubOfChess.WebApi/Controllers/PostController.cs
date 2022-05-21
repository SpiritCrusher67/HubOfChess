using AutoMapper;
using HubOfChess.Application.Posts.Commands.CreatePost;
using HubOfChess.Application.Posts.Commands.DeletePost;
using HubOfChess.Application.Posts.Queries.GetPostsByUserId;
using HubOfChess.Application.ViewModels;
using HubOfChess.WebApi.Models.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubOfChess.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class PostController : BaseController
    {
        private readonly IMapper mapper;

        public PostController(IMediator mediator, IMapper mapper) 
            : base(mediator)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the list of Posts that user has created
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /posts/1/10
        /// </remarks>
        /// <returns>Returns List of PostVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{page:int/pageLimit:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PostVM>>> GetAll(int page, int pageLimit)
        {
            var query = new GetPostsByUserIdQuery(UserId, page, pageLimit);

            var userPosts = await Mediator.Send(query);

            return Ok(userPosts);
        }

        /// <summary>
        /// Create Post
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /post
        /// {
        ///     title: "Post Title",
        ///     text: "Text...",
        /// }
        /// </remarks>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePostDto createPostDto)
        {
            createPostDto.SetUserId(UserId);
            var command = mapper.Map<CreatePostCommand>(createPostDto);

            var chatId = await Mediator.Send(command);

            return Ok(chatId);
        }

        /// <summary>
        /// Deletes the Post by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /post/414F0080-D5FE-42BD-9FC8-533F44E19048
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If post with given id not found</response>
        /// <response code="451">If user is not creator of this post</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeletePostCommand(id, UserId);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
