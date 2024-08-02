using BTS.Core.Commands.Models.Bus;
using BTS.Core.Queries.Models.Bus;
using BTS.Core.Queries.Models.Driver;
using BTS.Domain.Constants;
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
        public BusesController(IMediator mediator) : base(mediator) { }

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
            await HandleRequestAsync(new CreateBusCommand(dto));

        [HttpPut("{busId}")]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PutUpdateBus(Guid busId, [FromBody] UpdateBusDto dto) =>
            await HandleRequestAsync(new UpdateBusCommand(busId, dto));

        [HttpPatch("{busId}")]
        [Authorize(Common.AUTHORIZE_ROLE_ADMIN)]
        public async Task<IActionResult> PatchUpdateBusStatus(Guid busId, [FromQuery] CommonStatus newStatus) =>
            await HandleRequestAsync(new UpdateBusStatusCommand(busId, newStatus));
    }
}
