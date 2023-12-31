using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.BaseViewModels
{
    public class SortingItemViewModel
    {
        public string Field { get; set; } = "Id";
        public SortingType SortingType { get; set; } = SortingType.Ascending;
    }
}
