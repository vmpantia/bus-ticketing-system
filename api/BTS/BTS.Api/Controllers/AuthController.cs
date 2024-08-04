using BTS.Core.Commands.Models.Auth;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Models.Dtos.Auth;
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
        public async Task<IActionResult> PostLoginUser([FromBody] LoginDto dto) =>
            await HandleRequestAsync(new LoginCommand(dto.UsernameOrEmail, dto.Password));
    }
}
