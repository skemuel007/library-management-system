using System.IdentityModel.Tokens.Jwt;
using System.Net;
using LibraryBackend.Application.Dtos.Response;
using LibraryBackend.Application.Features.Commands;
using LibraryBackend.Application.Utilities.Configurations;
using LibraryBackend.Application.Validators;
using LibraryBackend.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LibraryBackend.Application.Features.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseCommandResponse<LoginResult>>
{
    private readonly UserManager<User> _userManager;
    private readonly JwtHandler _jwtHandler;
    private ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(UserManager<User> userManager,
        JwtHandler jwtHandler,
        ILogger<LoginCommandHandler> logger)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _jwtHandler = jwtHandler ?? throw new ArgumentNullException(nameof(jwtHandler));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<BaseCommandResponse<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validator = new LoginRequestValidator(_userManager);
        var validationResult = await validator.ValidateAsync(request.LoginRequest);

        if (validationResult.IsValid == false)
        {
            return new BaseCommandResponse<LoginResult>()
            {
                Message = "Login failed with error messages",
                Errors = validationResult.Errors.Select(v => v.ErrorMessage).ToList(),
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
        }
        
        var user = await _userManager.FindByNameAsync(request.LoginRequest.Email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, request.LoginRequest.Password))
            return new BaseCommandResponse<LoginResult>()
            {
                Success = false,
                Message = "Invalid Email or Password.",
                StatusCode = HttpStatusCode.Unauthorized
            };
        
        var secToken = await _jwtHandler.GetTokenAsync(user);
        var jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
        
        var loginResponse = new LoginResult()
        {
            Token = jwt,
        };

        return new BaseCommandResponse<LoginResult>()
        {
            Success = true,
            Message = "Login successful",
            Data = loginResponse,
            StatusCode = HttpStatusCode.OK
        };
    }
}