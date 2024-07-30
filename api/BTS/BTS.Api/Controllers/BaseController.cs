using BTS.Core.Results;
using BTS.Core.Results.Errors;
using BTS.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator) =>
            _mediator = mediator;

        protected async Task<IActionResult> HandleRequestAsync<TRequest>(TRequest request)
            where TRequest : class
        {
            try
            {
                // Check if the request is NULL
                if (request is null) throw new ArgumentNullException(nameof(request));

                // Process the request using mediator
                var response = await _mediator.Send(request);

                // Check if the response is a Result
                if (!(response is Result result)) throw new Exception("Invalid result for a certain request.");

                return result switch
                {
                    { IsSuccess: false, Error: var error } when error!.Type == ErrorType.NotFound => NotFound(result),
                    { IsSuccess: false } => BadRequest(result),
                    _ => Ok(result)
                };
            }
            catch (Exception ex)
            {
                return BadRequest(Result.Failure(CommonError.Unexpected(ex)));
            }
        }
    }
}
