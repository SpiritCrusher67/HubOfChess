using HubOfChess.Application.Users.Queries.GetUserById;
using HubOfChess.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HubOfChess.WebApi.Controllers
{
    public class UserController : BaseController
    {

        public UserController(IMediator mediator) 
            : base(mediator) { }

        /// <summary>
        /// Gets the User by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /user/60E35BA9-5977-4C9A-8E39-46CDCAB8882E
        /// </remarks>
        /// <returns>Returns UserVM</returns>
        /// <response code="200">Success</response>
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
    }
}
