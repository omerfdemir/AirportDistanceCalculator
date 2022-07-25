using System.Linq.Expressions;
using MongoDB.Driver;
using Utils;

namespace DocumentDbModel.AirportDocument;

public interface IMongoDbCollectionRepository<TEntity> where TEntity : class
{
        string CollectionName { get; }

        long CountDocuments(FilterDefinition<TEntity> filter);

        Task<long> CountDocumentsAsync(FilterDefinition<TEntity> filter, CountOptions countOptions = null, CancellationToken cancellationToken = default);

        long CountDocuments(Expression<Func<TEntity, bool>> expression);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> expression, CountOptions countOptions = null, CancellationToken cancellationToken = default);

        IEnumerable<TEntity> Find(FilterDefinition<TEntity> filter);
        IFindFluent<TEntity, TEntity> FindFluent(Expression<Func<TEntity, bool>> expression, FindOptions findOptions = null);
        IFindFluent<TEntity, TEntity> FindFluent(FilterDefinition<TEntity> filter, FindOptions findOptions = null);
        Task<IAsyncCursor<TEntity>> FindFluentAsync(
            Expression<Func<TEntity, bool>> expression, 
            FindOptions<TEntity, TEntity> findOptions = null, 
            CancellationToken cancellationToken = default);
        Task<IAsyncCursor<TEntity>> FindFluentAsync(
            FilterDefinition<TEntity> filter, 
            FindOptions<TEntity, TEntity> findOptions = null, 
            CancellationToken cancellationToken = default);
        Task<IAsyncCursor<TEntity>> FindAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default);
        Task<IAsyncCursor<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> expression, 
            CancellationToken cancellationToken = default);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        List<TEntity> FindAndSortBy(FilterDefinition<TEntity> filter, Expression<Func<TEntity, object>> sortExpression);
        List<TEntity> FindAndSortByDescending(
            FilterDefinition<TEntity> filter, 
            Expression<Func<TEntity, object>> sortExpression);
        List<TEntity> FindAndSortBy(
            Expression<Func<TEntity, bool>> findExpression, 
            Expression<Func<TEntity, object>> sortExpression);
        List<TEntity> FindAndSortByDescending(
            Expression<Func<TEntity, bool>> findExpression, 
            Expression<Func<TEntity, object>> sortExpression);

        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Find many documents paged
        /// </summary>
        /// <typeparam name="TKey">The Key property to sort with.</typeparam>
        /// <param name="page">Page</param>
        /// <param name="itemsPerPage">Items per page.</param>
        /// <param name="filter">Search filter.</param>
        /// <param name="sortExpr">Sort expression.</param>
        /// <param name="isSortDescending">Is sort descending.</param>
        /// <returns>Paged list of documents.</returns>
        PagedList<TEntity> FindManyPaged<TKey>(
            int page, 
            int itemsPerPage, 
            Expression<Func<TEntity, bool>> filter, 
            Expression<Func<TEntity, TKey>> sortExpr, 
            bool isSortDescending);

        /// <summary>
        /// Find many documents paged asynchronously. 
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="itemsPerPage">Items per page</param>
        /// <param name="filter">Search filter.</param>
        /// <returns>Paged list of documents.</returns>
        PagedList<TEntity> FindManyPaged(int page, int itemsPerPage, Expression<Func<TEntity, bool>> filter);        

        /// <summary>
        /// The async methods don't work because the MongoDB driver does not implement async IQueryable.
        /// Find many documents paged asynchronously. 
        /// </summary>
        /// <typeparam name="TKey">The Key property to sort with.</typeparam>
        /// <param name="page">Page</param>
        /// <param name="itemsPerPage">Items per page.</param>
        /// <param name="filter">Search filter.</param>
        /// <param name="sortExpr">Sort expression.</param>
        /// <param name="isSortDescending">Is sort descending.</param>
        /// <returns>Paged list of documents.</returns>
        Task<PagedList<TEntity>> FindManyPagedAsync<TKey>(
            int page, 
            int itemsPerPage, 
            Expression<Func<TEntity, bool>> filter, 
            Expression<Func<TEntity, TKey>> sortExpr, 
            bool isSortDescending,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// The async methods don't work because the MongoDB driver does not implement async IQueryable.
        /// Find many documents paged asynchronously. 
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="itemsPerPage">Items per page</param>
        /// <param name="filter">Search filter.</param>
        /// <returns>Paged list of documents.</returns>
        Task<PagedList<TEntity>> FindManyPagedAsync(
            int page, 
            int itemsPerPage, 
            Expression<Func<TEntity, bool>> filter, 
            CancellationToken cancellationToken = default);

        TEntity FindOneAndReplace(FilterDefinition<TEntity> filter, TEntity replacement);

        Task<TEntity> FindOneAndReplaceAsync(
            FilterDefinition<TEntity> filter, 
            TEntity replacement,
            FindOneAndReplaceOptions<TEntity, TEntity> findOneAndReplaceOptions = null,
            CancellationToken cancellationToken = default);

        TEntity FindOneAndReplace(Expression<Func<TEntity, bool>> expression, TEntity replacement);

        Task<TEntity> FindOneAndReplaceAsync(
            Expression<Func<TEntity, bool>> expression, 
            TEntity replacement,
            FindOneAndReplaceOptions<TEntity, TEntity> findOneAndReplaceOptions = null,
            CancellationToken cancellationToken = default);

        TEntity FindOneAndUpdate(
            FilterDefinition<TEntity> filter,
            UpdateDefinition<TEntity> update,
            FindOneAndUpdateOptions<TEntity> option = null);

        TEntity FindOneAndUpdate(
            Expression<Func<TEntity, bool>> expression, 
            UpdateDefinition<TEntity> update, 
            FindOneAndUpdateOptions<TEntity> option = null);

        Task<TEntity> FindOneAndUpdateAsync(
            Expression<Func<TEntity, bool>> expression,
            UpdateDefinition<TEntity> update,
            FindOneAndUpdateOptions<TEntity, TEntity> findOneAndUpdateOptions = null,
            CancellationToken cancellationToken = default);

        Task<TEntity> FindOneAndUpdateAsync(
            FilterDefinition<TEntity> filter, 
            UpdateDefinition<TEntity> updateDefinition,
            FindOneAndUpdateOptions<TEntity> findOneAndUpdateOptions = null,
            CancellationToken cancellationToken = default);

        Task<ReplaceOneResult> ReplaceOneAsync(
            FilterDefinition<TEntity> filter, 
            TEntity replacement, 
            ReplaceOptions options, 
            CancellationToken cancellationToken = default);

        Task<ReplaceOneResult> ReplaceOneAsync(
            Expression<Func<TEntity, bool>> expression, 
            TEntity replacement, 
            ReplaceOptions options, 
            CancellationToken cancellationToken = default);

        UpdateResult UpdateOne(
            FilterDefinition<TEntity> filter, 
            UpdateDefinition<TEntity> update, 
            UpdateOptions updateOptions = null);

        Task<UpdateResult> UpdateOneAsync(
            FilterDefinition<TEntity> filter, 
            UpdateDefinition<TEntity> update,
            UpdateOptions updateOptions = null,
            CancellationToken cancellationToken = default);

        UpdateResult UpdateOne(
            Expression<Func<TEntity, bool>> expression, 
            UpdateDefinition<TEntity> update, 
            UpdateOptions updateOptions = null);

        Task<UpdateResult> UpdateOneAsync(
            Expression<Func<TEntity, bool>> expression, 
            UpdateDefinition<TEntity> update,
            UpdateOptions updateOptions = null,
            CancellationToken cancellationToken = default);

        UpdateResult UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);

        Task<UpdateResult> UpdateManyAsync(
            FilterDefinition<TEntity> filter, 
            UpdateDefinition<TEntity> update,
            UpdateOptions updateOptions = null,
            CancellationToken cancellationToken = default);

        UpdateResult UpdateMany(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update);

        Task<UpdateResult> UpdateManyAsync(
            Expression<Func<TEntity, bool>> expression, 
            UpdateDefinition<TEntity> update,
            UpdateOptions updateOptions = null,
            CancellationToken cancellationToken = default);

        ReplaceOneResult ReplaceOne(Expression<Func<TEntity, bool>> expression, TEntity replacement, ReplaceOptions updateOptions);

        ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions updateOptions); 

        DeleteResult DeleteOne(FilterDefinition<TEntity> filter);

        Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default);

        DeleteResult DeleteOne(Expression<Func<TEntity, bool>> expression);

        Task<DeleteResult> DeleteOneAsync(
            Expression<Func<TEntity, bool>> expression, 
            CancellationToken cancellationToken = default);

        DeleteResult DeleteMany(FilterDefinition<TEntity> filter);

        Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default);

        DeleteResult DeleteMany(Expression<Func<TEntity, bool>> expression);

        Task<DeleteResult> DeleteManyAsync(
            Expression<Func<TEntity, bool>> expression, 
            CancellationToken cancellationToken = default);

        void InsertOne(TEntity item);

        Task InsertOneAsync(TEntity item, InsertOneOptions insertOneOptions = null, CancellationToken cancellationToken = default);

        void InsertMany(IEnumerable<TEntity> items, InsertManyOptions insertOneOptions = null);

        Task InsertManyAsync(IEnumerable<TEntity> items, InsertManyOptions insertOneOptions = null, CancellationToken cancellationToken = default);

        BulkWriteResult<TEntity> BulkWrite(IEnumerable<WriteModel<TEntity>> requests);

        Task<BulkWriteResult<TEntity>> BulkWriteAsync(
            IEnumerable<WriteModel<TEntity>> requests, 
            BulkWriteOptions bulkWriteOptions = null,
            CancellationToken cancellationToken = default);
    }
    