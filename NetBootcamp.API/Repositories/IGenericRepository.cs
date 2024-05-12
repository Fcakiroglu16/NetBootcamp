using NetBootcamp.API.Products;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NetBootcamp.API.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<IReadOnlyList<T>> GetAll();

        Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAllByPage(int page, int pageSize);

        Task Update(T entity);


        Task<T> Create(T entity);

        Task<T?> GetById(int id);

        Task Delete(int id);

        Task<bool> HasExist(int id);
    }
}