using LibraryBackend.Application.Interfaces.Persistence;
using LibraryBackend.Core.Entities;
using LibraryBackend.Infrastructure.Context;

namespace LibraryBackend.Infrastructure.Repositories;

public class BookRepository : GenericRepository<Book, Guid>, IBookRepository
{
    public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}