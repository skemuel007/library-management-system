namespace LibraryBackend.Application.Dtos.Request;

public record LoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}