namespace NetBootcamp.API.Repositories
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}