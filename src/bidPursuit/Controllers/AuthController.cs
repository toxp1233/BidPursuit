using bidPursuit.Application.Auth.Commands.Login;
using bidPursuit.Application.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bidPursuit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var result = await mediator.Send(registerCommand);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand registerCommand)
        {
            var result = await mediator.Send(registerCommand);
            return Ok(result);
        }
    }
}
