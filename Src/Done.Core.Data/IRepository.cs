using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Done.Core.Data
{
    public interface IRepository<TEntity> : IDisposable
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);
    }
}
