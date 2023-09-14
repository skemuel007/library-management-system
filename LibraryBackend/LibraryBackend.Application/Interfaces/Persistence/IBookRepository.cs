using LibraryBackend.Core.Entities;

namespace LibraryBackend.Application.Interfaces.Persistence;

public interface IBookRepository : IGenericRepository<Book, Guid>
{
    
}