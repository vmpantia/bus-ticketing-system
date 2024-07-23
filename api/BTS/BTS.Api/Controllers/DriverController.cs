using BTS.Core.Commands.Models;
using BTS.Core.Queries.Models.Driver;
using BTS.Domain.Models.Dtos.Driver;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    [ApiController]
    [Route("drivers")]
    public class DriverController : BaseController
    {
        public DriverController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IActionResult> GetDrivers() =>
            await HandleRequestAsync(new GetDriversQuery());

        [HttpGet("{driverId}")]
        public async Task<IActionResult> GetDriver(Guid driverId) =>
            await HandleRequestAsync(new GetDriverByIdQuery(driverId));

        [HttpPost]
        public async Task<IActionResult> PostCreateDriver([FromBody] CreateDriverDto dto) =>
            await HandleRequestAsync(new CreateDriverCommand(dto));
    }
}
