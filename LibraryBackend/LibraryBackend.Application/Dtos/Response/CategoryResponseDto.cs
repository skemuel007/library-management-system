using LibraryBackend.Application.Dtos.Common;
using LibraryBackend.Core.Entities;

namespace LibraryBackend.Application.Dtos.Response;

public class CategoryResponseDto : AuditBaseDto
{
    public string Name { get; set; }
    public Status Status { get; set; }
}