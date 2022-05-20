using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HubOfChess.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; }
        protected Guid UserId => User?.Identity?.IsAuthenticated == true 
            ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value) 
            : Guid.Empty;

        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
