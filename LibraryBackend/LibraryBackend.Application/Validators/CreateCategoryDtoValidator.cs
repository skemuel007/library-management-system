using FluentValidation;
using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Interfaces.Persistence;

namespace LibraryBackend.Application.Validators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryDtoValidator(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        
        RuleFor(b => b.Name)
            .NotEmpty().NotNull()
            .WithMessage("{PropertyName} is required.")
            .MustAsync(async (name, token) =>
            {
                var categoryNameExists = await _categoryRepository.AnyAsync(b => b.Name.ToLower() == name.ToLower());
                return !categoryNameExists;
            }).WithMessage("{PropertyName} exists.");
    }
}