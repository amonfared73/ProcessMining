using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessMining.Core.Domain.Models;

namespace ProcessMining.Infra.EntityFramework.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
