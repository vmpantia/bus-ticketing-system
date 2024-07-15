using BTS.Domain.Models.Enums;
using BTS.Domain.Results;
using BTS.Domain.Results.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BTS.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator) =>
            _mediator = mediator;

        protected async Task<IActionResult> HandleRequestAsync<TRequest, TResult>(TRequest request)
            where TRequest : class
            where TResult : class
        {
            try
            {
                // Check if the request is NULL
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                // Process the request using mediator
                var response = await _mediator.Send(request);

                // Check if the response is valid type of Result<TResult>
                if (!(response is ApiResponse<TResult> result))
                    throw new Exception("Invalid api response for a certain request.");

                return result switch
                {
                    { IsSuccess: false, Error: var error } when error!.Type == ErrorType.NotFound => NotFound(result),
                    { IsSuccess: false } => BadRequest(result),
                    _ => Ok(result)
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<TResult>.Failure(CommonError.Unexpected(ex)));
            }
        }
    }
}
