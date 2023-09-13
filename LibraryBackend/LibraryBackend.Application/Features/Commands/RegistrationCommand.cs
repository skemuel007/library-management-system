using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Dtos.Response;
using MediatR;

namespace LibraryBackend.Application.Features.Commands;

public class RegistrationCommand : IRequest<BaseCommandResponse>
{
    public RegistrationRequest RegistrationRequest { get; set; }
}