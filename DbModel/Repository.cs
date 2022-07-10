using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DbModel
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields
        protected DbContext _db;
        protected DbSet<TEntity> _set;
        #endregion // Fields

        #region Constructors
        internal Repository(DbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }
            _db = db;
            _set = _db.Set<TEntity>();
        }
        #endregion // Constructors

        #region Public methods
        public void Insert(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            _set.Add(obj);
        }

        public async Task InsertAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            await _set.AddAsync(obj, cancellationToken);
        }

        public void InsertMany(IEnumerable<TEntity> obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            _set.AddRange(obj);
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> obj, CancellationToken cancellationToken = default)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            await _set.AddRangeAsync(obj, cancellationToken);
        }

        public void Update(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            EntityState state = _db.Entry(obj).State;
            if (state == EntityState.Detached)
            {
                _set.Attach(obj);
                _db.Entry(obj).State = EntityState.Modified;
            }
        }

        public void Delete(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            EntityState state = _db.Entry(obj).State;
            if (state == EntityState.Detached)
            {
                _set.Attach(obj);
            }
            _set.Remove(obj);
        }

        public bool Any(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, false);
            bool result = query.Any();
            return result;
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, false);
            bool result = await query.AnyAsync(cancellationToken);
            return result;
        }

        public TEntity Find(object id)
        {
            TEntity e = _set.Find(id);
            return e;
        }

        public async Task<TEntity> FindAsync(object id, CancellationToken cancellationToken = default)
        {
            object[] ids =
            {
                id,
            };

            TEntity e = await _set.FindAsync(ids, cancellationToken);
            return e;
        }

        public TEntity FindOne(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool isTracking = true)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, isTracking);
            TEntity e = query.SingleOrDefault();
            return e;
        }

        public async Task<TEntity> FindOneAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool isTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, isTracking);
            TEntity e = await query.SingleOrDefaultAsync(cancellationToken);
            return e;
        }

        public TEntity FindFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool isTracking = true)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, isTracking);
            TEntity e = query.FirstOrDefault();
            return e;
        }

        public async Task<TEntity> FindFirstAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool isTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, isTracking);
            TEntity e = await query.FirstOrDefaultAsync(cancellationToken);
            return e;
        }

        public TEntity FindFirst<TKey>(
            Expression<Func<TEntity, TKey>> sortExpr,
            bool isSortDescending = false, Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool isTracking = true)
        {
            IQueryable<TEntity> query = GetQueryable(filter, sortExpr, isSortDescending, include, isTracking);
            TEntity e = query.FirstOrDefault();
            return e;
        }

        public async Task<TEntity> FindFirstAsync<TKey>(
            Expression<Func<TEntity, TKey>> sortExpr,
            bool isSortDescending = false,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool isTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, sortExpr, isSortDescending, include, isTracking);
            TEntity e = await query.FirstOrDefaultAsync(cancellationToken);
            return e;
        }

        public TEntity FindLast(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool isTracking = true)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, isTracking);
            TEntity e = query.LastOrDefault();
            return e;
        }

        public async Task<TEntity> FindLastAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool isTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, isTracking);
            TEntity e = await query.LastOrDefaultAsync(cancellationToken);
            return e;
        }

        public TEntity FindLast<TKey>(Expression<Func<TEntity, TKey>> sortExpr, bool isSortDescending = false, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool isTracking = true)
        {
            IQueryable<TEntity> query = GetQueryable(filter, sortExpr, isSortDescending, include, isTracking);
            TEntity e = query.LastOrDefault();
            return e;
        }

        public async Task<TEntity> FindLastAsync<TKey>(
            Expression<Func<TEntity, TKey>> sortExpr,
            bool isSortDescending = false,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool isTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, sortExpr, isSortDescending, include, isTracking);
            TEntity e = await query.LastOrDefaultAsync(cancellationToken);
            return e;
        }

        public List<TEntity> FindMany(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool isTracking = false)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, isTracking);
            List<TEntity> entities = query.ToList();
            return entities;
        }

        public async Task<List<TEntity>> FindManyAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, include, isTracking);
            List<TEntity> entities = await query.ToListAsync(cancellationToken);
            return entities;
        }

        public List<TEntity> FindMany<TKey>(Expression<Func<TEntity, TKey>> sortExpr, bool isSortDescending, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool isTracking = false)
        {
            IQueryable<TEntity> query = GetQueryable(filter, sortExpr, isSortDescending, include, isTracking);
            List<TEntity> entities = query.ToList();
            return entities;
        }

        public async Task<List<TEntity>> FindManyAsync<TKey>(
            Expression<Func<TEntity, TKey>> sortExpr,
            bool isSortDescending,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, sortExpr, isSortDescending, include, isTracking);
            List<TEntity> entities = await query.ToListAsync(cancellationToken);
            return entities;
        }
        public List<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool isTracking = false)
        {
            IQueryable<TEntity> query = GetQueryable(null, include, isTracking);
            List<TEntity> entities = query.ToList();
            return entities;
        }

        public async Task<List<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(null, include, isTracking);
            List<TEntity> entities = await query.ToListAsync(cancellationToken);
            return entities;
        }

        public List<TEntity> GetAll<TKey>(Expression<Func<TEntity, TKey>> sortExpr, bool isSortDescending = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool isTracking = false)
        {
            IQueryable<TEntity> query = GetQueryable(null, sortExpr, isSortDescending, include, isTracking);
            List<TEntity> entities = query.ToList();
            return entities;
        }

        public async Task<List<TEntity>> GetAllAsync<TKey>(
            Expression<Func<TEntity, TKey>> sortExpr,
            bool isSortDescending = false,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool isTracking = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(null, sortExpr, isSortDescending, include, isTracking);
            List<TEntity> entities = await query.ToListAsync(cancellationToken);
            return entities;
        }
        public void Detach(TEntity t)
        {
            _db.Entry(t).State = EntityState.Detached;
        }
        #endregion // Public methods

        #region Protected methods
        protected virtual IQueryable<TEntity> GetQueryable<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> sortExpr, bool isSortDescending, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool isTracking)
        {
            IQueryable<TEntity> query = _set;

            if (isTracking == false)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (sortExpr != null)
            {
                if (isSortDescending == false)
                {
                    query = query.OrderBy(sortExpr);
                }
                else
                {
                    query = query.OrderByDescending(sortExpr);
                }
            }

            return query;
        }

        protected virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool isTracking)
        {
            IQueryable<TEntity> query = _set;

            if (isTracking == false)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        #endregion // Protected methods
    }
}
