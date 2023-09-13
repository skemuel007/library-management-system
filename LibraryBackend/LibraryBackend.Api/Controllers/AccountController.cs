using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Dtos.Response;
using LibraryBackend.Application.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Api.Controllers;
public class AccountController : BaseController<AccountController>
{
    /*private readonly IMediator _mediator;
    
    public AccountController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }*/
    
    [Route("login", Name = "Login")]
    [HttpPost]
    public async Task<IActionResult> LoginUserAsync(LoginRequest loginRequest)
    {
        var response = await _mediator.Send(new LoginCommand() { LoginRequest = loginRequest});
        return ResolveActionResult(response);
    }
    
    [Route("register", Name = "Registration")]
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(RegistrationRequest registrationRequest)
    {
        var response = await _mediator.Send(new RegistrationCommand () { RegistrationRequest = registrationRequest});
        return ResolveActionResult(response);
    }
}