using BTS.Domain.Results;
using BTS.Domain.Results.Errors;
using BTS.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BTS.Domain.Contractors.Authentication;
using System.IdentityModel.Tokens.Jwt;
using BTS.Api.Extensions;
using BTS.Domain.Constants;
using BTS.Domain.Exceptions;

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
                return _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_USER_EMAIL, token) ?? "System";
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
            catch (NotFoundException ex)
            {
                return NotFound(Result.Failure(CommonError.NotFound(ex)));
            }
            catch (Exception ex)
            {
                return BadRequest(Result.Failure(CommonError.Unexpected(ex)));
            }
        }
    }
}
