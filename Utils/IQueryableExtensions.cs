using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Utils;

public static class IQueryableExtensions
{
            public static PagedList<T> GetPaged<T>(this IQueryable<T> query, int page = 1, int itemsPerPage = 10) where T : class
        {
            if (page < 1)
            {
                string errMsg = $"ERROR: Page is {page}, must be {1} or greater.";
                throw new ArgumentException(errMsg);
            }

            if (itemsPerPage < 1)
            {
                string errMsg = $"ERROR: Items per is {itemsPerPage}, must be {1} or greater.";
                throw new ArgumentException(errMsg);
            }

            PagedList<T> result = new PagedList<T>();
            result.CurrentPage = page;
            result.ItemsPerPage = itemsPerPage;

            //
            // Don't call this on IEnumerable. Then it will not execute in the DB.
            // calling it on query, the result will execute in the DB.
            //
            result.TotalItems = query.Count();

            double totalPages = (double)result.TotalItems / result.ItemsPerPage;
            result.TotalPages = (int)Math.Ceiling(totalPages);

            int skip = (page - 1) * result.ItemsPerPage;
            result.Items = query.Skip(skip).Take(result.ItemsPerPage).ToList();

            return result;
        }

        public static async Task<PagedList<T>> GetPagedAsync<T>(
            this IQueryable<T> query, 
            int page = 1, 
            int itemsPerPage = 1,
            CancellationToken cancellationToken = default) where T : class
        {
            if (page < 1)
            {
                string errMsg = $"ERROR: Page is {page}, must be {1} or greater.";
                throw new ArgumentException(errMsg);
            }
            
            if (itemsPerPage < 1)
            {
                string errMsg = $"ERROR: Items per is {itemsPerPage}, must be {1} or greater.";
                throw new ArgumentException(errMsg);
            }

            PagedList<T> result = new PagedList<T>();
            result.CurrentPage = page;
            result.ItemsPerPage = itemsPerPage;

            //
            // Don't call this on IEnumerable. Then it will not execute in the DB.
            // calling it on query, the result will execute in the DB.
            //
            result.TotalItems = await query.CountAsync();

            double totalPages = (double)result.TotalItems / result.ItemsPerPage;
            result.TotalPages = (int)Math.Ceiling(totalPages);

            int skip = (page - 1) * result.ItemsPerPage;
            result.Items = await query.Skip(skip).Take(result.ItemsPerPage).ToListAsync(cancellationToken);

            return result;
        }
    }