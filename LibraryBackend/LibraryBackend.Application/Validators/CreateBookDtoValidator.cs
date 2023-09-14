using System.Security.Cryptography;
using FluentValidation;
using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Interfaces.Persistence;

namespace LibraryBackend.Application.Validators;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;
    public CreateBookDtoValidator(IBookRepository bookRepository,
        ICategoryRepository categoryRepository)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        RuleFor(b => b.Title)
            .NotEmpty().NotNull()
            .WithMessage("{PropertyName} is required.");
        
        RuleFor(b => b.ISBN)
            .NotEmpty().NotNull()
            .WithMessage("{PropertyName} is required.")
            .MustAsync(async (ISBN, token) =>
            {
                var bookExists = await _bookRepository.AnyAsync(b => b.ISBN == ISBN);
                return !bookExists;
            }).WithMessage("{PropertyName} exists.");

        RuleFor(b => b.CategoryId)
            .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
            .MustAsync(async (categoryId, token) =>
            {
                var categoryExists = await _categoryRepository.AnyAsync(c => c.Id == categoryId);
                return categoryExists;
            });

    }
    
}