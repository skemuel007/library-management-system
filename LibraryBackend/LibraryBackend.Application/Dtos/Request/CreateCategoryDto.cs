using LibraryBackend.Core.Entities;

namespace LibraryBackend.Application.Dtos.Request;

public record CreateCategoryDto
{
    public string Name { get; set; }
    public Status Status { get; set; }
}