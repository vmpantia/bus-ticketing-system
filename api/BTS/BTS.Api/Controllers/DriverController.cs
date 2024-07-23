using BTS.Core.Queries.Models.Driver;
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
    }
}
