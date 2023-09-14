using LibraryBackend.Core.Entities;

namespace LibraryBackend.Application.Interfaces.Persistence;

public interface ICategoryRepository : IGenericRepository<Category, Guid>
{
    
}