namespace LibraryBackend.Core.Common;

public class AuditBaseEntity<T> : BaseEntity<T>
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}