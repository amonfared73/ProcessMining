using ProcessMining.Core.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.Models
{
    public class Car : DomainObject
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return string.Format("Car name: {0}", Name);
        }
    }
}
