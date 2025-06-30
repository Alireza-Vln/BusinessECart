namespace BusinessECart.Contracts.Interfaces;

public interface IUnitOfWork
{
    Task Complete();
     void Save();
    Task Begin();
    Task Rollback();
    Task Commit();
}