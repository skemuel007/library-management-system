using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Dtos.Response;
using MediatR;

namespace LibraryBackend.Application.Features.Commands;

public class LoginCommand : IRequest<BaseCommandResponse<LoginResult>>
{
    public LoginRequest LoginRequest { get; set; }
}