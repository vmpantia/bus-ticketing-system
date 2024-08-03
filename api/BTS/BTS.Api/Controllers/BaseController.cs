using BTS.Domain.Results;
using BTS.Domain.Results.Errors;
using BTS.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BTS.Domain.Contractors.Authentication;
using System.IdentityModel.Tokens.Jwt;
using BTS.Api.Extensions;

namespace BTS.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IJwtProvider _jwtProvider;
        public BaseController(IMediator mediator, IJwtProvider jwtProvider)
        {
            _mediator = mediator;
            _jwtProvider = jwtProvider;
        }

        public string Email 
        {
            get
            {
                var token = HttpContext.GetBearerToken();
                return _jwtProvider.GetValueByClaim(JwtRegisteredClaimNames.Email, token) ?? "System";
            }
        }

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
