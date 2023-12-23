using Microsoft.IdentityModel.Tokens;
using ProcessMining.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.Queries
{
    public class AccessTokenGenerator
    {
        public string GenerateToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2034c25fb98b48bdf3a5f4147cfa373e71cb22c906a9d6697cc8d22a8f5b5831"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            JwtSecurityToken token = new JwtSecurityToken(
                "https://locahost:7231", 
                "https://locahost:7231", 
                claims, 
                DateTime.UtcNow, 
                DateTime.UtcNow.AddMinutes(30),
                credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
