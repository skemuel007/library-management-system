namespace LibraryBackend.Application.Dtos.Response;

public record LoginResult
{
    /// <summary>
    /// The JWT token if the login attempt is successful, or NULL if not /// </summary>
    public string? Token { get; set; }
}