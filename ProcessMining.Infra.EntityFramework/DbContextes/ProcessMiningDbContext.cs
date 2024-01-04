using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.Models;

namespace ProcessMining.Infra.EntityFramework.DbContextes
{
    public class ProcessMiningDbContext : DbContext
    {
        public ProcessMiningDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProcessMiningDbContext).Assembly);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<RefreshToken> RefreshTokens{ get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
