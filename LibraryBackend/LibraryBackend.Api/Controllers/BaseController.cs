using System.Net;
using LibraryBackend.Application.Dtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[ApiVersion("1.0")]
public class BaseController<T> : ControllerBase
{
    private IMediator _mediatorInstance;
    
    private ILogger<T> _loggerInstance;
    //private IResponse_Request _response_request;
    protected IMediator _mediator => _mediatorInstance ??=(_mediatorInstance = HttpContext.RequestServices.GetService<IMediator>());

    protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();

    protected IActionResult ResolveActionResult<R>(R response) where R : BaseCommandResponse
    {
        IActionResult res;
        
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                res = Ok(response);
                break;
            
            case HttpStatusCode.BadRequest:
                res = BadRequest(response);
                break;
            
            case HttpStatusCode.Created:
                res = StatusCode(StatusCodes.Status201Created, response as R);
                break;
            
            case HttpStatusCode.NotFound:
                res = NotFound(response);
                break;
            
            case HttpStatusCode.Unauthorized:
                res = Unauthorized(response);
                break;
            
            case HttpStatusCode.Forbidden:
                res = StatusCode(StatusCodes.Status403Forbidden, response as R);
                break;
            
            default:
                res = StatusCode(StatusCodes.Status500InternalServerError, response as R);
                break;
        }

        return res;
    }
    
}