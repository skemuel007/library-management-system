using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace LibraryBackend.Application.Behaviours;

public class LoggingDelegatingHandler : DelegatingHandler
{
    private readonly ILogger<LoggingDelegatingHandler> _logger;

    public LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Sending request to {Url}", request.RequestUri);
            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Received a success response from {Url}", response?.RequestMessage?.RequestUri);
            }
            else
            {
                _logger.LogWarning("Received a non-success status code {StatusCode} from {Url}", (int)response.StatusCode, response?.RequestMessage?.RequestUri);
            }

#pragma warning disable CS8603 // Possible null reference return.
            return response;
#pragma warning restore CS8603 // Possible null reference return.
        }
        catch (HttpRequestException ex)
            when (ex.InnerException is SocketException se && se.SocketErrorCode == SocketError.ConnectionRefused)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var hostWithPort = request.RequestUri.IsDefaultPort ? request.RequestUri.DnsSafeHost
                : $"{request.RequestUri.DnsSafeHost}:{request.RequestUri.Port}";
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _logger.LogCritical(ex, "Unable to connect to {Host}. Please check the configuration to ensure the correct URL for the service has been configured", hostWithPort);
        }

        return new HttpResponseMessage(System.Net.HttpStatusCode.BadGateway)
        {
            RequestMessage = request
        };
    }

}