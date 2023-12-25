﻿using Microsoft.IdentityModel.Tokens;
using ProcessMining.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;

        public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public string GenerateToken(User user)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            return _tokenGenerator.GenerateToken(
                _configuration.AccessTokenSecret, 
                _configuration.Issuer, 
                _configuration.Audience, 
                _configuration.AccessTokenExpirationMinutes, 
                claims);

        }
    }
}
