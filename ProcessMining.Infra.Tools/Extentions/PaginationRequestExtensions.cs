using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Infra.Tools.Extentions
{
    public static class PaginationRequestExtensions
    {
        public static PaginationRequestViewModel Fix(this PaginationRequestViewModel model)
        {
            if (model.PageNumber == 0 || model.RecordsPerPage == 0)
            {
                model.PageNumber = 1;
                model.RecordsPerPage = 5;
            }
            return model;
        }

        public static Pagination ToPagination(this PaginationRequestViewModel request, int totalRecords)
        {
            return new Pagination()
            {
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling((decimal)totalRecords / request.RecordsPerPage),
                TotalRecords = totalRecords,
            };
        }
    }
}
