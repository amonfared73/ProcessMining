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
            request.Fix();
            return source.Skip((request.PageNumber - 1) * request.RecordsPerPage).Take(request.RecordsPerPage);
        }
    }
}
