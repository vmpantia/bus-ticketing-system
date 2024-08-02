using BTS.Core.Commands.Models.Driver;
using BTS.Core.Queries.Models.Driver;
using BTS.Domain.Constants;
using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DriversController : BaseController
    {
        public DriversController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> GetDrivers() =>
            await HandleRequestAsync(new GetDriversQuery());

        [HttpGet("{driverId}")]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> GetDriver(Guid driverId) =>
            await HandleRequestAsync(new GetDriverByIdQuery(driverId));

        [HttpPost]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PostCreateDriver([FromBody] CreateDriverDto dto) =>
            await HandleRequestAsync(new CreateDriverCommand(dto));

        [HttpPut("{driverId}")]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PutUpdateDriver(Guid driverId, [FromBody] UpdateDriverDto dto) =>
            await HandleRequestAsync(new UpdateDriverCommand(driverId, dto));

        [HttpPatch("{driverId}")]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PatchUpdateDriverStatus(Guid driverId, [FromQuery] CommonStatus newStatus) =>
            await HandleRequestAsync(new UpdateDriverStatusCommand(driverId, newStatus));
    }
}
