using ProcessMining.Core.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.Models
{
    public class User : DomainObject
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
