using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.BaseViewModels
{
    public class BaseRequestViewModel
    {
        public PaginationRequestViewModel? PaginationRequest { get; set; }
        public SortingRequestViewModel? SortingRequest { get; set; }
        public SearchTermViewModel? SearchTermRequest { get; set; }
    }
}
