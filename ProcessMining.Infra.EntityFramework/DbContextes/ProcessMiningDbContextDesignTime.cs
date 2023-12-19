using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Infra.EntityFramework.DbContextes
{
    public class ProcessMiningDbContextDesignTime : IDesignTimeDbContextFactory<ProcessMiningDbContext>
    {
        public ProcessMiningDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=ProcessMining.db";
            var optionsBuilder = new DbContextOptionsBuilder().UseSqlite(connectionString);
            return new ProcessMiningDbContext(optionsBuilder.Options);
        }
    }
}
