using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.BaseViewModels;
using ProcessMining.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Infra.Tools.Extentions
{
    public static class IEnumerableExtensions
    {
        private static IEnumerable<T> ApplySearchTerm<T>(this IEnumerable<T> source, SearchTermViewModel request)
        {
            return !string.IsNullOrWhiteSpace(request.SearchTerm) ? source.Where(t => t.ToString().ToLower().Contains(request.SearchTerm.ToLower())) : source;
        }
        private static IEnumerable<T> ApplySorting<T>(this IEnumerable<T> source, SortingRequestViewModel request)
        {
            var property = typeof(T).GetProperty(request.SortingItem.Field);
            if (!string.IsNullOrWhiteSpace(request.SortingItem.Field) || property != null)
                return request.SortingItem.SortingType == SortingType.Ascending ? source.OrderBy(t => t.GetType().GetProperty(property.Name).GetValue(t)) : source.OrderByDescending(t => t.GetType().GetProperty(property.Name).GetValue(t));
            else
                return source;
        }
        private static IEnumerable<T> ApplyPagination<T>(this IEnumerable<T> source, PaginationRequestViewModel request)
        {
            request.FixPagination();
            return source.Skip((request.PageNumber - 1) * request.RecordsPerPage).Take(request.RecordsPerPage);
        }
        public static IEnumerable<T> ApplyBaseReuest<T>(this IEnumerable<T> source, BaseRequestViewModel request)
        {
            return source.ApplySearchTerm(request.SearchTermRequest).ApplySorting(request.SortingRequest).ApplyPagination(request.PaginationRequest);
        }
    }
}
