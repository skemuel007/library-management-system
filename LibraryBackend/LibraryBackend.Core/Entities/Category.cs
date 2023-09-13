using LibraryBackend.Core.Common;

namespace LibraryBackend.Core.Entities;

public class Category : AuditBaseEntity<Guid>
{
    public string Name { get; set; }
    public Status Status { get; set; }
    
    public ICollection<Book> Books { get; set; }
}