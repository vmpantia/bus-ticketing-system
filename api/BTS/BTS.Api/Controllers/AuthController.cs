using BTS.Core.Commands.Models.Authentication;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Models.Dtos.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, IJwtProvider jwtProvider) : base(mediator, jwtProvider) { }

        [HttpPost("login")]
        public async Task<IActionResult> PostLoginByCredentials([FromBody] LoginByCredentialsDto dto) =>
            await HandleRequestAsync(new LoginByCredentialsCommand(dto.UsernameOrEmail, dto.Password));

        [HttpPost("email/login")]
        public async Task<IActionResult> PostLoginByEmail([FromBody] LoginByEmailDto dto) =>
            await HandleRequestAsync(new LoginByEmailCommand(dto.Email));

        [HttpPost("token/login")]
        public async Task<IActionResult> PostLoginByToken([FromQuery] string token) =>
            await HandleRequestAsync(new LoginByTokenCommand(token));
    }
}
