using System.Linq.Expressions;
using LibraryBackend.Application.Dtos.Response;
using LibraryBackend.Application.Specifications;
using LibraryBackend.Core.Common;
using LibraryBackend.Core.Entities;

namespace LibraryBackend.Application.Interfaces.Persistence;

public interface IBookRepository : IGenericRepository<Book, Guid>
{
    
}

public interface IGenericRepository<T, IdType> where T : BaseEntity<IdType>
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAsEnumerableAsync(Expression<Func<T, bool>> predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
        List<Expression<Func<T, object>>> includes = null, 
        bool disableTracking = true);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

    Task<Paginated<T>> GetWherePaginated(PaginateQueryRequest<T> queryRequest);
    Task<Paginated<T>> GetPaginated(PaginateQueryRequestNew<T> queryRequest);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeString = null,
        bool disableTracking = true);
    
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true);

    Task<T> GetByIdAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<bool> AddMultipleAsync(IEnumerable<T> entities);

    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true);
    void Update(T entity);
    void DeleteAsync(T entity);
}