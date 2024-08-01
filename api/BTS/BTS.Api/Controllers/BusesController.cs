using BTS.Core.Commands.Models.Bus;
using BTS.Domain.Models.Dtos.Bus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : BaseController
    {
        public BusesController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> PostCreateBus([FromBody] CreateBusDto dto) =>
            await HandleRequestAsync(new CreateBusCommand(dto));
    }
}
