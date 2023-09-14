using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Dtos.Response;
using MediatR;

namespace LibraryBackend.Application.Features.Commands;

public class CreateCategoryCommand : IRequest<BaseCommandResponse<CategoryResponseDto>>
{
    public CreateCategoryDto CategoryDto { get; set; }
}