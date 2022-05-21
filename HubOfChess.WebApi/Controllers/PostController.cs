using AutoMapper;
using HubOfChess.Application.PostLikes.Commands.CreatePostLike;
using HubOfChess.Application.PostLikes.Commands.DeletePostLike;
using HubOfChess.Application.PostLikes.Queries.GetPostLikesByPostId;
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
        /// GET /post/1/10
        /// </remarks>
        /// <returns>Returns List of PostVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{page:int/pageLimit:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PostVM>>> GetAllPosts(int page, int pageLimit)
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
        public async Task<ActionResult<Guid>> CreatePost([FromBody] CreatePostDto createPostDto)
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
        public async Task<ActionResult> DeletePost(Guid id)
        {
            var command = new DeletePostCommand(id, UserId);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Gets the list of Post Likes by Post id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /post/like/414F0080-D5FE-42BD-9FC8-533F44E19048
        /// </remarks>
        /// <returns>Returns List of PostLikeVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If post with given id not found</response>
        [HttpGet("like/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PostVM>>> GetAllLikes(Guid id)
        {
            var query = new GetPostLikesByPostIdQuery(id);

            var postLikes = await Mediator.Send(query);

            return Ok(postLikes);
        }

        /// <summary>
        /// Create Post Like
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /post/like/414F0080-D5FE-42BD-9FC8-533F44E19048
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If the user is already liked post</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("like/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateLike(Guid id)
        {
            var command = new CreatePostLikeCommand(UserId, id);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes the Post Like by Post id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /post/like/414F0080-D5FE-42BD-9FC8-533F44E19048
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If like with given post id not found</response>
        [HttpDelete("like/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteLike(Guid id)
        {
            var command = new DeletePostLikeCommand(UserId, id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
