using FluentValidation;
using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryBackend.Application.Validators;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    private readonly UserManager<User> _userManager;
    
    public RegistrationRequestValidator(UserManager<User> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        RuleFor(l => l.FirstName)
            .NotEmpty().NotNull()
            .WithMessage("{PropertyName} is required.");
        
        RuleFor(l => l.LastName)
            .NotEmpty().NotNull()
            .WithMessage("{PropertyName} is required.");
        
        RuleFor(l => l.Email)
            .NotEmpty().NotNull()
            .WithMessage("{PropertyName} is required.")
            .MustAsync(async (email, token) =>
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user is null;
            }).WithMessage("{PropertyName} already exists");
        
        RuleFor(l => l.Password)
            .NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
        
        RuleFor(vm => vm.ConfirmPassword)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Equal(vm => vm.Password).WithMessage("Passwords do not match");
    }
}