using LibraryBackend.Application.Interfaces.Persistence;
using LibraryBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryBackend.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed = false;
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        
    }

    public virtual void Dispose(bool disposing)
    {
        if(!this._disposed)
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }

        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction.RollbackAsync();
    }
}