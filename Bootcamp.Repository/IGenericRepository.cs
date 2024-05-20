using System.Linq.Expressions;

namespace Bootcamp.Repository
{
    public interface IGenericRepository<T, in T2>
    {
        Task<IReadOnlyList<T>> GetAll();

        Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAllByPage(int page, int pageSize);

        Task Update(T entity);


        Task<T> Create(T entity);

        Task<T?> GetById(T2 id);

        Task Delete(T2 id);

        Task<bool> HasExist(T2 id);
    }
}