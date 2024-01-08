using ProcessMining.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Infra.Tools.Extentions
{
    public static class StringExtensions
    {
        public static BaseOperations ToBaseOperation(this string str)
        {
            return Enum.Parse<BaseOperations>(str);
        }
    }
}
