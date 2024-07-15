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
            await HandleRequestAsync<GetDriversQuery, IEnumerable<DriverDto>>(new GetDriversQuery());
    }
}
