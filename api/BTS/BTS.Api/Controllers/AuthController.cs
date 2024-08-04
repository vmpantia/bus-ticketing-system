using BTS.Core.Commands.Models.User;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Models.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, IJwtProvider jwtProvider) : base(mediator, jwtProvider) { }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> PostLoginUser([FromBody] LoginUserDto dto) =>
            await HandleRequestAsync(new LoginUserCommand(dto.UsernameOrEmail, dto.Password));
    }
}
