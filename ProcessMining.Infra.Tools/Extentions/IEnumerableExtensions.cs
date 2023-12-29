using ProcessMining.Core.Domain.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Infra.Tools.Extentions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ApplyPagination<T>(this IEnumerable<T> source, PaginationRequestViewModel request)
        {
            request.FixPagination();
            return source.Skip((request.PageNumber - 1) * request.RecordsPerPage).Take(request.RecordsPerPage);
        }
        public static IEnumerable<T> ApplySearchTerm<T>(this IEnumerable<T> source, SearchTermViewModel request)
        {
            return !string.IsNullOrWhiteSpace(request.SearchTerm) ? source.Where(t => t.ToString().Contains(request.SearchTerm)) : source;
        }
    }
}
