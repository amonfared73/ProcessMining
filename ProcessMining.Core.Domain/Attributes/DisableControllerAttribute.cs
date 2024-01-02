using ProcessMining.Core.Domain.Attributes.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.Attributes
{
    public class DisableControllerAttribute : Attribute, IDisableControllerConvention
    {
    }
}
