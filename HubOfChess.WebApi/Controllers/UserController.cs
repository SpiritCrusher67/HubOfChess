using AutoMapper;
using HubOfChess.Application.ChatInvites.Queries.GetChatInvitesByUserId;
using HubOfChess.Application.FriendInvites.Commands.AcceptFriendInvite;
using HubOfChess.Application.FriendInvites.Commands.CreateFriendInvite;
using HubOfChess.Application.FriendInvites.Queries.GetFriendInvitesByUserId;
using HubOfChess.Application.Users.Queries.GetUserById;
using HubOfChess.Application.ViewModels;
using HubOfChess.WebApi.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubOfChess.WebApi.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IMapper mapper;

        public UserController(IMediator mediator, IMapper mapper) 
            : base(mediator)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the User by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /user/60E35BA9-5977-4C9A-8E39-46CDCAB8882E
        /// </remarks>
        /// <returns>Returns UserVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If user with given id not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserVM>> Get(Guid id)
        {
            var query = new GetUserByIdQuery(id);

            var user = await Mediator.Send(query);

            return Ok(user);
        }

        /// <summary>
        /// Gets User Friends Invites
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /user/friend/invites
        /// </remarks>
        /// <returns>Returns list of FriendInviteVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("friend/invites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<FriendInviteVM>>> GetFriendInvites()
        {
            var query = new GetFriendInvitesByUserIdQuery(UserId);

            var userFriendInvites = await Mediator.Send(query);

            return Ok(userFriendInvites);
        }

        /// <summary>
        /// Gets User Chat Invites
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /user/chat/invites
        /// </remarks>
        /// <returns>Returns list of ChatInviteVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("chat/invites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ChatInviteVM>>> GetChatInvites()
        {
            var query = new GetChatInvitesByUserIdQuery(UserId);

            var userChatInvites = await Mediator.Send(query);

            return Ok(userChatInvites);
        }

        /// <summary>
        /// Create Friend Invite
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /friend/invite
        /// {
        ///     invitedUserId: "414F0080-D5FE-42BD-9FC8-533F44E19048",
        ///     message: "Invite message"
        /// }
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="201">Success</response>
        /// <response code="400">If the Invite to given user is already exist</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If user with given id not found</response>
        [HttpPost("friend/invite")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateInvite([FromBody] CreateFriendInviteDto createFriendInviteDto)
        {
            createFriendInviteDto.SetUserId(UserId);
            var command = mapper.Map<CreateFriendInviteCommand>(createFriendInviteDto);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Accepts the Friend Invite by Sender User id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT user/friend/invite/414F0080-D5FE-42BD-9FC8-533F44E19048
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If invite with given user id not found</response>
        /// <response code="451">If user is not invited to this user friends</response>
        [HttpPut("friend/invite/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<ActionResult> AcceptInvite(Guid id)
        {
            var command = new AcceptFriendInviteCommand(UserId, id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
