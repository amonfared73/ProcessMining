using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.Domain.Models
{
    public class AuthenticationConfiguration
    {
        public string AccessTokenSecret { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
