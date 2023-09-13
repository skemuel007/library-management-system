namespace LibraryBackend.Application.Dtos.Common;

public abstract class AuditBaseDto : BaseDto
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}