﻿using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Core.Domain.Models;
using ProcessMining.Infra.EntityFramework.DbContextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class AuthenticationService : BaseService<Authentication>, IAuthenticationService
    {
        private readonly IDbContextFactory<ProcessMiningDbContext> _contextFactory;

        public AuthenticationService(IDbContextFactory<ProcessMiningDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
