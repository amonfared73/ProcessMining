using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.BaseModels
{
    public class Pagination
    {
        public int CurrentPage { get; set; } = 1;
        public int PerPage { get; set; } = 10;
        public long? TotalCount { get; set; }
        public int? PageCount => (TotalCount > 0) ? ((int)Math.Ceiling((double)(TotalCount / ((PerPage > 0) ? PerPage : 10)).Value)) : 0;
    }
}
