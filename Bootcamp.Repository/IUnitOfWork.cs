namespace Bootcamp.Repository
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}