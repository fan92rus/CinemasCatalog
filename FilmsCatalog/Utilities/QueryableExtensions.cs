using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalog.Services
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> GetPagedResultAsync<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            var skip = (currentPage - 1) * pageSize;
            var take = pageSize;

            var rowCount = await query.CountAsync();
            var results = await query.Skip(skip).Take(take).ToListAsync();

            var pagedResult = new PagedResult<T>
            {
                CurrentPage = currentPage,
                PageCount = (int)Math.Ceiling(decimal.Divide(rowCount, pageSize)),
                PageSize = pageSize,
                RowCount = rowCount,
                Results = results
            };

            return pagedResult;
        }
    }
}