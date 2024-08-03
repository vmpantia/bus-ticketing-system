using BTS.Core.Commands.Models.Bus;
using BTS.Core.Queries.Models.Bus;
using BTS.Core.Queries.Models.Driver;
using BTS.Domain.Constants;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Models.Dtos.Bus;
using BTS.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BusesController : BaseController
    {
        public BusesController(IMediator mediator, IJwtProvider jwtProvider) : base(mediator, jwtProvider) { }

        [HttpGet]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> GetBuses() =>
            await HandleRequestAsync(new GetDriversQuery());

        [HttpGet("{busId}")]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> GetBus(Guid busId) =>
            await HandleRequestAsync(new GetBusByIdQuery(busId));

        [HttpPost]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PostCreateBus([FromBody] CreateBusDto dto) =>
            await HandleRequestAsync(new CreateBusCommand(dto, Email));

        [HttpPut("{busId}")]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PutUpdateBus(Guid busId, [FromBody] UpdateBusDto dto) =>
            await HandleRequestAsync(new UpdateBusCommand(busId, dto, Email));

        [HttpPatch("{busId}")]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PatchUpdateBusStatus(Guid busId, [FromQuery] CommonStatus newStatus) =>
            await HandleRequestAsync(new UpdateBusStatusCommand(busId, newStatus, Email));
    }
}
