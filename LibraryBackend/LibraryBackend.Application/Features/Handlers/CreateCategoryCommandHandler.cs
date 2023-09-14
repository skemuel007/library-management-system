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

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseCommandResponse<CategoryResponseDto>>
{
    private ILogger<CreateCategoryCommandHandler> _logger;
    private ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ILogger<CreateCategoryCommandHandler> logger,
        ICategoryRepository categoryRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<BaseCommandResponse<CategoryResponseDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCategoryDtoValidator(_categoryRepository);
        var validationResult = await validator.ValidateAsync(request.CategoryDto);

        if (validationResult.IsValid == false)
        {
            return new BaseCommandResponse<CategoryResponseDto>()
            {
                Message = "Category creation failed with error messages",
                Errors = validationResult.Errors.Select(v => v.ErrorMessage).ToList(),
                StatusCode = HttpStatusCode.UnprocessableEntity
            };
        }

        var category = _mapper.Map<Category>(request.CategoryDto);
        category = await _categoryRepository.AddAsync(category);

        await _unitOfWork.CompleteAsync();

        var categoryResponse = _mapper.Map<CategoryResponseDto>(category);

        return new BaseCommandResponse<CategoryResponseDto>
        {
            Message = "Successfully created book category",
            StatusCode = HttpStatusCode.Created,
            Data = categoryResponse,
            Success = true
        };
    }
}