using AutoMapper;
using HubOfChess.Application.Chats.Commands.CreateChat;
using HubOfChess.Application.Chats.Commands.DeleteChat;
using HubOfChess.Application.Chats.Commands.LeaveChat;
using HubOfChess.Application.Chats.Commands.UpdateChat;
using HubOfChess.Application.Chats.Queries.GetChatById;
using HubOfChess.Application.Chats.Queries.GetChatsByUserId;
using HubOfChess.Application.ViewModels;
using HubOfChess.WebApi.Models.Chat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubOfChess.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class ChatController : BaseController
    {
        private readonly IMapper mapper;

        public ChatController(IMediator mediator, IMapper mapper) 
            : base(mediator)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the list of Chats with user is a member
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /chat
        /// </remarks>
        /// <returns>Returns List of ChatVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ChatVM>>> GetAll()
        {
            var query = new GetChatsByUserIdQuery(UserId);

            var userChats = await Mediator.Send(query);

            return Ok(userChats);
        }

        /// <summary>
        /// Gets the Chat by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /chat/60E35BA9-5977-4C9A-8E39-46CDCAB8882E
        /// </remarks>
        /// <returns>Returns ChatVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If chat with given id not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChatVM>> Get(Guid id)
        {
            var query = new GetChatByIdQuery(id, UserId);

            var chat = await Mediator.Send(query);

            return Ok(chat);
        }

        /// <summary>
        /// Create Chat
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /chat
        /// {
        ///     chatName: "name"
        /// }
        /// </remarks>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateChatDto createChatDto)
        {
            createChatDto.SetUserId(UserId);
            var command = mapper.Map<CreateChatCommand>(createChatDto);

            var chatId = await Mediator.Send(command);

            return Ok(chatId);
        }

        /// <summary>
        /// Updates the Chat 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /chat
        /// {
        ///     chatId: "414F0080-D5FE-42BD-9FC8-533F44E19048",
        ///     chatName: "name",
        ///     newChatOwnerId: "D6CAE4CA-E57C-42E6-88DF-0351ED6CE901"
        /// }
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If chat with given id not found</response>
        /// <response code="451">If user is not owner of this chat</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<ActionResult> Update([FromBody] UpdateChatDto updateChatDto)
        {
            updateChatDto.SetUserId(UserId);
            var command = mapper.Map<UpdateChatCommand>(updateChatDto);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes the Chat by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /chat/414F0080-D5FE-42BD-9FC8-533F44E19048
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If chat with given id not found</response>
        /// <response code="451">If user is not owner of this chat</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteChatCommand(id, UserId);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Removes user from Chat members by Chat id. 
        /// If user is owner then first user on members list becomes an owner.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /chat/leave/414F0080-D5FE-42BD-9FC8-533F44E19048
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If chat with given id not found</response>
        /// <response code="451">If user is not member of this chat</response>
        [HttpPost]
        [Route("leave/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<ActionResult> Leave(Guid id)
        {
            var command = new LeaveChatCommand(id, UserId);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
