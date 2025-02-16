using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class PaginatedResult<T> where T : class
    {
        private static int minPageNumber = 1;
        private static int maxPageSize = 50;
        public IReadOnlyList<T> Items { get; }

        public int TotalCount {  get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginatedResult(IQueryable<T> query, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < minPageNumber ? minPageNumber : pageNumber;
            PageSize = pageSize < 1 || pageSize > 50 ? maxPageSize : pageSize;
            TotalCount = query.Count();
            Items = query.Skip(this.PageSize * (this.PageNumber - 1)).Take(this.PageSize).ToList();
        }
    }
}
