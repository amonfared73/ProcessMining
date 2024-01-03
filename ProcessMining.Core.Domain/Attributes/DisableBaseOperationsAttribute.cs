﻿using ProcessMining.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DisableBaseOperationsAttribute : Attribute
    {
        public IEnumerable<BaseOperations> Operations { get; set; }

        public DisableBaseOperationsAttribute(IEnumerable<BaseOperations> operations)
        {
            Operations = operations;
        }
    }
}