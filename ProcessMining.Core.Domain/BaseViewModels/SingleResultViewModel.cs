using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.BaseViewModels
{
    public class SingleResultViewModel<T> where T : DomainObject
    {
        public T? Entity { get; set; }
        public ResponseMessage ResponseMessage { get; set; }
    }
}
