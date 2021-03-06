using System.Collections.Generic;

namespace FilmsCatalog.Services
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public ICollection<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}