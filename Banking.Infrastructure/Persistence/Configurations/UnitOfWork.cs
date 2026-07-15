using Banking.Domain.Interfaces;

namespace Banking.Infrastructure.Persistence;

public class UnitOfWork(
    BankingDbContext context)
    : IUnitOfWork
{
    private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction?
        _transaction;


    public async Task BeginTransactionAsync()
    {
        _transaction = await context.Database
            .BeginTransactionAsync();
    }


    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();

        if (_transaction != null)
        {
            await _transaction.CommitAsync();
        }
    }


    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
        }
    }
}