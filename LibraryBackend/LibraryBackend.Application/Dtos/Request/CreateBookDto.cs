namespace LibraryBackend.Application.Dtos.Request;

public record CreateBookDto
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int PublishedYear { get; set;  }
    public string Genre { get; set; }
    public string ImageUrl { get; set; }

    public Guid CategoryId { get; set; }
}