using System.Reflection.Metadata.Ecma335;
using LibraryBackend.Core.Common;

namespace LibraryBackend.Core.Entities;

public class Book : AuditBaseEntity<Guid>
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int PublishedYear { get; set;  }
    public string Genre { get; set; }
    public string ImageUrl { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}