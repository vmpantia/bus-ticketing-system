using BTS.Core.Commands.Models.User;
using BTS.Domain.Constants;
using BTS.Domain.Contractors.Authentication;
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
        public UsersController(IMediator mediator, IJwtProvider jwtProvider) : base(mediator, jwtProvider) { }

        [HttpPost]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PostCreateUserAdmin([FromBody] CreateUserDto dto) =>
            await HandleRequestAsync(new CreateUserCommand(dto, true, Email));
    }
}
