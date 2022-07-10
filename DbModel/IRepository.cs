using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DbModel
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);

        Task<TEntity> FindOneAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool isTracking = true,
            CancellationToken cancellationToken = default);

        Task InsertManyAsync(IEnumerable<TEntity> obj, CancellationToken cancellationToken = default);

        void Delete(TEntity obj);

        void Update(TEntity obj);
    }
}
