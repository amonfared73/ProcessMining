using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Infra.EntityFramework.DbContextes
{
    public class ProcessMiningDbContextFactory : IDbContextFactory<ProcessMiningDbContext>
    {
        private readonly DbContextOptions _options;

        public ProcessMiningDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public ProcessMiningDbContext CreateDbContext()
        {
            return new ProcessMiningDbContext(_options);
        }
    }
}
