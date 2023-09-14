using LibraryBackend.Application.Interfaces.Persistence;
using LibraryBackend.Core.Entities;
using LibraryBackend.Infrastructure.Context;

namespace LibraryBackend.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}