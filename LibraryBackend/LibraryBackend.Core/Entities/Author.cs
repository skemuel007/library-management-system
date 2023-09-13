using LibraryBackend.Core.Common;

namespace LibraryBackend.Core.Entities;

public class Author : AuditBaseEntity<Guid>
{
    public string Name { get; set; }
}