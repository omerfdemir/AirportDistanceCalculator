using System.Linq.Expressions;
using MongoDB.Driver;
using Utils;

namespace DocumentDbModel.AirportDocument;

    public class MongoDbCollectionRepository<TEntity> : IMongoDbCollectionRepository<TEntity> where TEntity : class
    {
        protected IMongoCollection<TEntity> _mongoCollection;
        protected string _collectionName;

        public MongoDbCollectionRepository(IMongoCollection<TEntity> mongoCollection, string collectionName)
        {
            _mongoCollection = mongoCollection;
            _collectionName = collectionName;
        }

        public virtual string CollectionName
        {
            get { return _collectionName; }
        }

        public virtual long CountDocuments(FilterDefinition<TEntity> filter)
        {
            long result = _mongoCollection.CountDocuments(filter);
            return result;
        }

        public virtual async Task<long> CountDocumentsAsync(FilterDefinition<TEntity> filter, CountOptions countOptions = null, CancellationToken cancellationToken = default)
        {
            long result = await _mongoCollection.CountDocumentsAsync(filter, countOptions, cancellationToken);
            return result;
        }

        public virtual long CountDocuments(Expression<Func<TEntity, bool>> expression)
        {
            long result = _mongoCollection.CountDocuments(expression);
            return result;
        }

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> expression, CountOptions countOptions = null, CancellationToken cancellationToken = default)
        {
            long result = await _mongoCollection.CountDocumentsAsync(expression, countOptions, cancellationToken);
            return result;
        }

        public virtual IEnumerable<TEntity> Find(FilterDefinition<TEntity> filter)
        {
            IEnumerable<TEntity> result = _mongoCollection.Find(filter).ToEnumerable();
            return result;
        }

        public virtual IFindFluent<TEntity, TEntity> FindFluent(Expression<Func<TEntity, bool>> expression, FindOptions findOptions = null)
        {
            IFindFluent<TEntity, TEntity> result = _mongoCollection.Find(expression, findOptions);

            return result;
        }

        public virtual IFindFluent<TEntity, TEntity> FindFluent(FilterDefinition<TEntity> filter, FindOptions findOptions = null)
        {
            IFindFluent<TEntity, TEntity> result = _mongoCollection.Find(filter, findOptions);

            return result;
        }

        public virtual async Task<IAsyncCursor<TEntity>> FindFluentAsync(Expression<Func<TEntity, bool>> expression,
            FindOptions<TEntity, TEntity> findOptions = null,
            CancellationToken cancellationToken = default)
        {
            IAsyncCursor<TEntity> result = await _mongoCollection.FindAsync(expression, findOptions, cancellationToken);

            return result;
        }

        public virtual async Task<IAsyncCursor<TEntity>> FindFluentAsync(FilterDefinition<TEntity> filter,
            FindOptions<TEntity, TEntity> findOptions = null,
            CancellationToken cancellationToken = default)
        {
            IAsyncCursor<TEntity> result = await _mongoCollection.FindAsync(filter, findOptions, cancellationToken);

            return result;
        }

        public virtual async Task<IAsyncCursor<TEntity>> FindAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
        {
            IAsyncCursor<TEntity> result = await _mongoCollection.FindAsync(filter, null, cancellationToken);
            return result;
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            IEnumerable<TEntity> result = _mongoCollection.Find(expression).ToEnumerable();
            return result;
        }

        public virtual async Task<IAsyncCursor<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            IAsyncCursor<TEntity> result = await _mongoCollection.FindAsync(expression, null, cancellationToken);
            return result;
        }

        public virtual List<TEntity> FindAndSortBy(FilterDefinition<TEntity> filter, Expression<Func<TEntity, object>> sortExpression)
        {
            List<TEntity> result = _mongoCollection.Find(filter).SortBy(sortExpression).ToList();
            return result;
        }

        public virtual List<TEntity> FindAndSortByDescending(FilterDefinition<TEntity> filter, Expression<Func<TEntity, object>> sortExpression)
        {
            List<TEntity> result = _mongoCollection.Find(filter).SortByDescending(sortExpression).ToList();
            return result;
        }

        public virtual List<TEntity> FindAndSortBy(Expression<Func<TEntity, bool>> findExpression, Expression<Func<TEntity, object>> sortExpression)
        {
            List<TEntity> result = _mongoCollection.Find(findExpression).SortBy(sortExpression).ToList();
            return result;
        }

        public virtual List<TEntity> FindAndSortByDescending(Expression<Func<TEntity, bool>> findExpression, Expression<Func<TEntity, object>> sortExpression)
        {
            List<TEntity> result = _mongoCollection.Find(findExpression).SortByDescending(sortExpression).ToList();
            return result;
        }

        // public static IOrderedFindFluent<TDocument, TProjection> ThenBy<TDocument, TProjection>(this IOrderedFindFluent<TDocument, TProjection> find, Expression<Func<TDocument, object>> field);
        // public static IOrderedFindFluent<TDocument, TProjection> ThenByDescending<TDocument, TProjection>(this IOrderedFindFluent<TDocument, TProjection> find, Expression<Func<TDocument, object>> field);


        public virtual IQueryable<TEntity> AsQueryable()
        {
            IQueryable<TEntity> result = _mongoCollection.AsQueryable();
            return result;
        }

        public virtual PagedList<TEntity> FindManyPaged<TKey>(int page, int itemsPerPage, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> sortExpr, bool isSortDescending)
        {
            IQueryable<TEntity> query = GetQueryable(filter, sortExpr, isSortDescending);
            PagedList<TEntity> entities = query.GetPaged(page, itemsPerPage);
            return entities;
        }

        public virtual PagedList<TEntity> FindManyPaged(int page, int itemsPerPage, Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = GetQueryable(filter);
            PagedList<TEntity> entities = query.GetPaged(page, itemsPerPage);
            return entities;
        }

        public virtual async Task<PagedList<TEntity>> FindManyPagedAsync<TKey>(int page,
            int itemsPerPage,
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> sortExpr,
            bool isSortDescending,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter, sortExpr, isSortDescending);
            PagedList<TEntity> entities = await query.GetPagedAsync(page, itemsPerPage, cancellationToken);
            return entities;
        }

        public virtual async Task<PagedList<TEntity>> FindManyPagedAsync(int page, int itemsPerPage, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = GetQueryable(filter);
            PagedList<TEntity> entities = await query.GetPagedAsync(page, itemsPerPage, cancellationToken);
            return entities;
        }

        public virtual TEntity FindOneAndReplace(FilterDefinition<TEntity> filter, TEntity replacement)
        {
            TEntity result = _mongoCollection.FindOneAndReplace(filter, replacement);
            return result;
        }

        public virtual async Task<TEntity> FindOneAndReplaceAsync(FilterDefinition<TEntity> filter,
            TEntity replacement,
            FindOneAndReplaceOptions<TEntity, TEntity> findOneAndReplaceOptions = null,
            CancellationToken cancellationToken = default)
        {
            TEntity result = await _mongoCollection.FindOneAndReplaceAsync(filter, replacement, findOneAndReplaceOptions, cancellationToken);

            return result;
        }

        public virtual TEntity FindOneAndReplace(Expression<Func<TEntity, bool>> expression, TEntity replacement)
        {
            TEntity result = _mongoCollection.FindOneAndReplace(expression, replacement);
            return result;
        }

        public virtual async Task<TEntity> FindOneAndReplaceAsync(Expression<Func<TEntity, bool>> expression,
            TEntity replacement,
            FindOneAndReplaceOptions<TEntity, TEntity> findOneAndReplaceOptions = null,
            CancellationToken cancellationToken = default)
        {
            TEntity result = await _mongoCollection.FindOneAndReplaceAsync(expression, replacement, findOneAndReplaceOptions, cancellationToken);

            return result;
        }

        public virtual TEntity FindOneAndUpdate(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity> option = null)
        {
            if (option == null)
            {
                option = new FindOneAndUpdateOptions<TEntity>
                {
                    ReturnDocument = ReturnDocument.After
                };
            }

            TEntity result = _mongoCollection.FindOneAndUpdate(filter, update, option);
            return result;
        }

        public virtual async Task<TEntity> FindOneAndUpdateAsync(Expression<Func<TEntity, bool>> expression,
            UpdateDefinition<TEntity> updateDefinition,
            FindOneAndUpdateOptions<TEntity, TEntity> findOneAndUpdateOptions = null,
            CancellationToken cancellationToken = default)
        {
            if (findOneAndUpdateOptions == null)
            {
                findOneAndUpdateOptions = new FindOneAndUpdateOptions<TEntity, TEntity>
                {
                    ReturnDocument = ReturnDocument.After
                };
            }

            return await _mongoCollection.FindOneAndUpdateAsync(expression, updateDefinition, findOneAndUpdateOptions, cancellationToken);
        }

        public virtual async Task<TEntity> FindOneAndUpdateAsync(FilterDefinition<TEntity> filter,
            UpdateDefinition<TEntity> update,
            FindOneAndUpdateOptions<TEntity> findOneAndUpdateOptions = null,
            CancellationToken cancellationToken = default)
        {
            if (findOneAndUpdateOptions == null)
            {
                findOneAndUpdateOptions = new FindOneAndUpdateOptions<TEntity>
                {
                    ReturnDocument = ReturnDocument.After
                };
            }

            TEntity result = await _mongoCollection.FindOneAndUpdateAsync(filter, update, findOneAndUpdateOptions, cancellationToken);
            return result;
        }

        public virtual TEntity FindOneAndUpdate(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity> option = null)
        {
            if (option == null)
            {
                option = new FindOneAndUpdateOptions<TEntity>
                {
                    ReturnDocument = ReturnDocument.After
                };
            }

            TEntity result = _mongoCollection.FindOneAndUpdate(expression, update, option);
            return result;
        }

        public virtual async Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            ReplaceOneResult result = await _mongoCollection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
            return result;
        }

        public virtual async Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<TEntity, bool>> expression,
            TEntity replacement,
            ReplaceOptions options = null,
            CancellationToken cancellationToken = default)
        {
            ReplaceOneResult result = await _mongoCollection.ReplaceOneAsync(expression, replacement, options, cancellationToken);
            return result;
        }

        public virtual UpdateResult UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions updateOptions = null)
        {
            UpdateResult result = _mongoCollection.UpdateOne(filter, update, updateOptions);
            return result;
        }

        public virtual async Task<UpdateResult> UpdateOneAsync(FilterDefinition<TEntity> filter,
            UpdateDefinition<TEntity> update,
            UpdateOptions updateOptions = null,
            CancellationToken cancellationToken = default)
        {
            UpdateResult result = await _mongoCollection.UpdateOneAsync(filter, update, updateOptions, cancellationToken);
            return result;
        }

        public virtual UpdateResult UpdateOne(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update, UpdateOptions updateOptions = null)
        {
            UpdateResult result = _mongoCollection.UpdateOne(expression, update, updateOptions);
            return result;
        }

        public virtual async Task<UpdateResult> UpdateOneAsync(Expression<Func<TEntity, bool>> expression,
            UpdateDefinition<TEntity> update,
            UpdateOptions updateOptions = null,
            CancellationToken cancellationToken = default)
        {
            UpdateResult result = await _mongoCollection.UpdateOneAsync(expression, update, updateOptions, cancellationToken);
            return result;
        }

        public virtual UpdateResult UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            UpdateResult result = _mongoCollection.UpdateMany(filter, update);
            return result;
        }

        public virtual async Task<UpdateResult> UpdateManyAsync(FilterDefinition<TEntity> filter,
            UpdateDefinition<TEntity> update,
            UpdateOptions updateOptions = null,
            CancellationToken cancellationToken = default)
        {
            UpdateResult result = await _mongoCollection.UpdateManyAsync(filter, update, updateOptions, cancellationToken);
            return result;
        }

        public virtual UpdateResult UpdateMany(Expression<Func<TEntity, bool>> expression, UpdateDefinition<TEntity> update)
        {
            UpdateResult result = _mongoCollection.UpdateMany(expression, update);
            return result;
        }

        public virtual async Task<UpdateResult> UpdateManyAsync(Expression<Func<TEntity, bool>> expression,
            UpdateDefinition<TEntity> update,
            UpdateOptions updateOptions = null,
            CancellationToken cancellationToken = default)
        {
            UpdateResult result = await _mongoCollection.UpdateManyAsync(expression, update, updateOptions, cancellationToken);
            return result;
        }

        public virtual ReplaceOneResult ReplaceOne(Expression<Func<TEntity, bool>> expression, TEntity replacement, ReplaceOptions options = null)
        {
            ReplaceOneResult result = _mongoCollection.ReplaceOne(expression, replacement, options);
            return result;
        }

        public virtual ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null)
        {
            ReplaceOneResult result = _mongoCollection.ReplaceOne(filter, replacement, options);
            return result;
        }

        public virtual DeleteResult DeleteOne(FilterDefinition<TEntity> filter)
        {
            DeleteResult result = _mongoCollection.DeleteOne(filter);
            return result;
        }

        public virtual async Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
        {
            DeleteResult result = await _mongoCollection.DeleteOneAsync(filter, cancellationToken);
            return result;
        }

        public virtual DeleteResult DeleteOne(Expression<Func<TEntity, bool>> expression)
        {
            DeleteResult result = _mongoCollection.DeleteOne(expression);
            return result;
        }

        public virtual async Task<DeleteResult> DeleteOneAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            DeleteResult result = await _mongoCollection.DeleteOneAsync(expression, cancellationToken);
            return result;
        }

        public virtual DeleteResult DeleteMany(FilterDefinition<TEntity> filter)
        {
            DeleteResult result = _mongoCollection.DeleteMany(filter);
            return result;
        }

        public virtual async Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
        {
            DeleteResult result = await _mongoCollection.DeleteManyAsync(filter, cancellationToken);
            return result;
        }

        public virtual DeleteResult DeleteMany(Expression<Func<TEntity, bool>> expression)
        {
            DeleteResult result = _mongoCollection.DeleteMany(expression);
            return result;
        }

        public virtual async Task<DeleteResult> DeleteManyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            DeleteResult result = await _mongoCollection.DeleteManyAsync(expression, cancellationToken);
            return result;
        }

        public virtual void InsertOne(TEntity item)
        {
            _mongoCollection.InsertOne(item);
        }

        public virtual async Task InsertOneAsync(TEntity item, InsertOneOptions insertOneOptions = null, CancellationToken cancellationToken = default)
        {
            await _mongoCollection.InsertOneAsync(item, insertOneOptions, cancellationToken);
        }

        public virtual void InsertMany(IEnumerable<TEntity> items, InsertManyOptions insertManyOptions = null)
        {
            _mongoCollection.InsertMany(items, insertManyOptions);
        }

        public virtual async Task InsertManyAsync(IEnumerable<TEntity> items, InsertManyOptions insertManyOptions = null, CancellationToken cancellationToken = default)
        {
            await _mongoCollection.InsertManyAsync(items, insertManyOptions, cancellationToken);
        }

        public virtual BulkWriteResult<TEntity> BulkWrite(IEnumerable<WriteModel<TEntity>> requests)
        {
            BulkWriteResult<TEntity> result = _mongoCollection.BulkWrite(requests);
            return result;
        }

        public virtual async Task<BulkWriteResult<TEntity>> BulkWriteAsync(IEnumerable<WriteModel<TEntity>> requests,
            BulkWriteOptions bulkWriteOptions = null,
            CancellationToken cancellationToken = default)
        {
            BulkWriteResult<TEntity> result = await _mongoCollection.BulkWriteAsync(requests, bulkWriteOptions, cancellationToken);
            return result;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> sortExpr, bool isSortDescending)
        {
            IQueryable<TEntity> query = AsQueryable();
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

        protected virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }