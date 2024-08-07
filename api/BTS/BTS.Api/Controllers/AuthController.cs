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

        [HttpPost("login/email")]
        public async Task<IActionResult> PostLoginByEmail(string email) =>
            await HandleRequestAsync(new LoginByEmailCommand(email));

        [HttpPost("login/token")]
        public async Task<IActionResult> PostLoginByToken([FromQuery] string token) =>
            await HandleRequestAsync(new LoginByTokenCommand(token));

        [HttpPost("reset/password")]
        public async Task<IActionResult> PostResetPasswordByEmail(string email) =>
            await HandleRequestAsync(new ResetPasswordByEmailCommand(email));

        [HttpPut("update/password")]
        public async Task<IActionResult> PutUpdatePassword([FromBody] UpdatePasswordDto dto) =>
            await HandleRequestAsync(new UpdatePasswordCommand(dto.Token, dto.NewPassword));
    }
}
