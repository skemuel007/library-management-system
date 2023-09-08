namespace LibraryBackend.Application.Utilities.Configurations;

public class CacheSetting
{
    public string ConnectionString { get; set; }
    public double AbsoluteExpirationInHours { get; set; }
    public double SlidingExpirationInMinutes { get; set; }
}