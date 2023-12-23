﻿using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Infra.EntityFramework.DbContextes
{
    public class ProcessMiningDbContext : DbContext
    {
        public ProcessMiningDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
    }
}