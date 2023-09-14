using System.Net;
using LibraryBackend.Application.Dtos.Response;
using LibraryBackend.Application.Features.Commands;
using LibraryBackend.Application.Validators;
using LibraryBackend.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LibraryBackend.Application.Features.Handlers;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, BaseCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private ILogger<RegistrationCommandHandler> _logger;

    public RegistrationCommandHandler(UserManager<User> userManager,
        ILogger<RegistrationCommandHandler> logger)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<BaseCommandResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var validator = new RegistrationRequestValidator(_userManager);
        var validationResult = await validator.ValidateAsync(request.RegistrationRequest);

        if (validationResult.IsValid == false)
        {
            return new BaseCommandResponse<LoginResult>()
            {
                Message = "User registration failed with error messages",
                Errors = validationResult.Errors.Select(v => v.ErrorMessage).ToList(),
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
        }
        
        var userRegistrationResponse = await _userManager.CreateAsync(
            new User() { UserName = request.RegistrationRequest.Email, 
                Email = request.RegistrationRequest.Email }, 
            request.RegistrationRequest.Password);
        
        if (!userRegistrationResponse.Succeeded)
        {
            return new BaseCommandResponse()
            {
                Message = "User Registration Failed",
                StatusCode = HttpStatusCode.BadRequest,
                Success = false
            };
        }
        
        return new BaseCommandResponse()
        {
            Message = $"User with email {request.RegistrationRequest.Email} created successfully",
            StatusCode = HttpStatusCode.Created,
            Success = true
        };
    }
}