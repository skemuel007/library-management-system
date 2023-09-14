using LibraryBackend.Application.Dtos.Common;

namespace LibraryBackend.Application.Dtos.Response;

public class BookResponseDto : AuditBaseDto
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int PublishedYear { get; set;  }
    public string Genre { get; set; }
    public string ImageUrl { get; set; }

    public Guid CategoryId { get; set; }
    
    public CategoryResponseDto Category { get; set; }
}