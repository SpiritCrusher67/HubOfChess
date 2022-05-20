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
    public class ChatController : BaseController
    {
        private readonly IMapper mapper;

        public ChatController(IMediator mediator, IMapper mapper) 
            : base(mediator)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatVM>>> GetAll()
        {
            var query = new GetChatsByUserIdQuery(UserId);

            var userChats = await Mediator.Send(query);

            return Ok(userChats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatVM>> Get(Guid id)
        {
            var query = new GetChatByIdQuery(id, UserId);

            var chat = await Mediator.Send(query);

            return Ok(chat);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateChatDto createChatDto)
        {
            createChatDto.SetUserId(UserId);
            var command = mapper.Map<CreateChatCommand>(createChatDto);

            var chatId = await Mediator.Send(command);

            return Ok(chatId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateChatDto updateChatDto)
        {
            updateChatDto.SetUserId(UserId);
            var command = mapper.Map<UpdateChatCommand>(updateChatDto);

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteChatCommand(id, UserId);

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost]
        [Route("leave/{id}")]
        public async Task<ActionResult<bool>> Leave(Guid id)
        {
            var command = new LeaveChatCommand(id, UserId);

            var isLeaved = await Mediator.Send(command);

            return Ok(isLeaved);
        }
    }
}
