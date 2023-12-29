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
        public int TotalPages { get; set; }
        public long? TotalRecords { get; set; }
    }
}
