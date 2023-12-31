﻿using ProcessMining.Core.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.BaseViewModels
{
    public class PagedResultViewModel<T> where T : DomainObject
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
