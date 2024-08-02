using BTS.Core.Commands.Models.Bus;
using BTS.Core.Commands.Models.User;
using BTS.Domain.Constants;
using BTS.Domain.Models.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator) { }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> PostLoginUser([FromBody] LoginUserDto dto) =>
            await HandleRequestAsync(new LoginUserCommand(dto.UsernameOrEmail, dto.Password));

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostCreateUserAdmin([FromBody] CreateUserDto dto) =>
            await HandleRequestAsync(new CreateUserCommand(dto, true));
    }
}
