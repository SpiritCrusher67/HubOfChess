using AutoMapper;
using HubOfChess.Application.Messages.Commands.CreateMessage;
using HubOfChess.Application.Messages.Commands.DeleteMessage;
using HubOfChess.Application.Messages.Queries.GetMessagesByChatId;
using HubOfChess.Application.ViewModels;
using HubOfChess.WebApi.Models.Message;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubOfChess.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class MessageController : BaseController
    {
        private readonly IMapper mapper;

        public MessageController(IMediator mediator, IMapper mapper) 
            : base(mediator)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the list of Messages that senden in given chat
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /message/EBD997DC-4123-4F3D-8F3F-37E40573F8B8/1/10
        /// </remarks>
        /// <returns>Returns List of MessageVM</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If chat with given id not found</response>
        /// <response code="451">If user is not member of given chat</response>
        [HttpGet("{id}/{page:int}/{pageLimit:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<ActionResult<IEnumerable<MessageVM>>> GetAll(Guid id, int page, int pageLimit)
        {
            var query = new GetMessagesByChatIdQuery(id,UserId,page,pageLimit);

            var chatMessages = await Mediator.Send(query);

            return Ok(chatMessages);
        }

        /// <summary>
        /// Create Message
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /message
        /// {
        ///     chatId: "77AACFAD-70AE-452C-AA6F-4BEBB2496603",
        ///     text: "Hello world"
        /// }
        /// </remarks>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If chat with given id not found</response>
        /// <response code="451">If user is not member of given chat</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateMessageDto createMessageDto)
        {
            createMessageDto.SetUserId(UserId);
            var command = mapper.Map<CreateMessageCommand>(createMessageDto);

            var chatId = await Mediator.Send(command);

            return Ok(chatId);
        }

        /// <summary>
        /// Deletes the Message by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /message/543F0080-D5FE-42HD-9FC8-533F44E19567
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If message with given id not found</response>
        /// <response code="451">If user is not sender of this message</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status451UnavailableForLegalReasons)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteMessageCommand(id, UserId);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
