

using BusinessECart.Contracts.Interfaces;

namespace BusinessECart.Persistence.EF;

public class EFUnitOfWork(EFDataContext dataContext) : IUnitOfWork
{
    public async Task Complete()
    {
        await dataContext.SaveChangesAsync();
    }

    public void Save()
    {
        dataContext.SaveChanges();
    }

    public async Task Begin()
    {
        await dataContext.Database.BeginTransactionAsync();
    }

    public async Task Rollback()
    {
        await dataContext.Database.RollbackTransactionAsync();
    }

    public async Task Commit()
    {
        await dataContext.SaveChangesAsync();
        await dataContext.Database.CommitTransactionAsync();
    }
}