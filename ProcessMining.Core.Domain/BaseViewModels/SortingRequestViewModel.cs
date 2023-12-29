using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.BaseViewModels
{
    public class SortingRequestViewModel
    {
        public IEnumerable<SortingItemViewModel> SortingItems { get; set; }
    }
}
