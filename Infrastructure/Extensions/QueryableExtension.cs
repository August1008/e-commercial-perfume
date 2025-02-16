using Core.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class QueryableExtension
    {
        public static PaginatedResult<T> Paginate<T>(this IQueryable<T> query, int page = 1, int pageSize = 50) where T : class
        {
            return new PaginatedResult<T>(query, page, pageSize);
        }
    }
}
