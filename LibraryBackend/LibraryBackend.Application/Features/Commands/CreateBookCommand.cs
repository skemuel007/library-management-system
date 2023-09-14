using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Dtos.Response;
using MediatR;

namespace LibraryBackend.Application.Features.Commands;

public class CreateBookCommand : IRequest<BaseCommandResponse<BookResponseDto>>
{
    public CreateBookDto BookDto { get; set; }
}