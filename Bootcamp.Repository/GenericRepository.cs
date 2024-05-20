using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Bootcamp.Repository
{
    public class GenericRepository<T, T2> : IGenericRepository<T, T2> where T : BaseEntity<T2> where T2 : struct
    {
        public DbSet<T> DbSet { get; set; }
        protected AppDbContext Context;

        public GenericRepository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }


        public async Task<IReadOnlyList<T>> GetAll()
        {
            var list = await DbSet.ToListAsync();
            return list.AsReadOnly();
        }

        public async Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            var list = await DbSet.Where(predicate).ToListAsync();
            return list.AsReadOnly();
        }

        public async Task<IReadOnlyList<T>> GetAllByPage(int page, int pageSize)
        {
            var list = await DbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return list.AsReadOnly();
        }

        public Task Update(T entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<T> Create(T entity)
        {
            await DbSet.AddAsync(entity);

            return entity;
        }

        public async Task<T?> GetById(T2 id)
        {
            var result = await DbSet.FindAsync(id);
            return result;
        }

        public async Task Delete(T2 id)
        {
            var entity = await GetById(id);

            DbSet.Remove(entity!);
        }

        public Task<bool> HasExist(T2 id)
        {
            // x.id== id

            return DbSet.AnyAsync(x => x.Id.Equals(id));

            //var entity = await GetById(id);

            //return entity is not null;
        }
    }
}