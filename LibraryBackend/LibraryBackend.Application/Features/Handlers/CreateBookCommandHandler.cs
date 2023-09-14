using System.Net;
using AutoMapper;
using LibraryBackend.Application.Dtos.Response;
using LibraryBackend.Application.Features.Commands;
using LibraryBackend.Application.Interfaces.Persistence;
using LibraryBackend.Application.Validators;
using LibraryBackend.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryBackend.Application.Features.Handlers;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BaseCommandResponse<BookResponseDto>>
{
    private ILogger<CreateCategoryCommandHandler> _logger;
    private ICategoryRepository _categoryRepository;
    private IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateBookCommandHandler(ILogger<CreateCategoryCommandHandler> logger,
        ICategoryRepository categoryRepository,
        IBookRepository bookRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    
    public async Task<BaseCommandResponse<BookResponseDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateBookDtoValidator(
            bookRepository: _bookRepository, categoryRepository: _categoryRepository);
        
        var validationResult = await validator.ValidateAsync(request.BookDto);

        if (validationResult.IsValid == false)
        {
            return new BaseCommandResponse<BookResponseDto>()
            {
                Message = "Book creation failed with error messages",
                Errors = validationResult.Errors.Select(v => v.ErrorMessage).ToList(),
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
        }
        
        var book = _mapper.Map<Book>(request.BookDto);
        book = await _bookRepository.AddAsync(book);

        await _unitOfWork.CompleteAsync();

        var bookResponse = _mapper.Map<BookResponseDto>(book);

        return new BaseCommandResponse<BookResponseDto>
        {
            Message = "Successfully add book to library",
            StatusCode = HttpStatusCode.Created,
            Data = bookResponse,
            Success = true
        };
    }
}