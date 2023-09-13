using FluentValidation;
using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryBackend.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    private readonly UserManager<User> _userManager;
    public LoginRequestValidator(UserManager<User> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        
        RuleFor(l => l.Email)
            .NotEmpty().NotNull()
            .WithMessage("{PropertyName} is required.")
            .MustAsync(async (email, token) =>
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user is not null;
            }).WithMessage("Invalid user credentials");
        RuleFor(l => l.Password)
            .NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
    }
}