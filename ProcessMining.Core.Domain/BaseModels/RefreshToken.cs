using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.BaseModels
{
    public class RefreshToken : DomainObject
    {
        //public Guid Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
