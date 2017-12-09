using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> entity)
        {
            entity.HasKey(test => test.Id);
            entity.Property(test => test.CreatedAt).IsRequired();
            entity.Property(test => test.Description).HasMaxLength(255).IsRequired();
            entity.Property(test => test.Name).HasMaxLength(255).IsRequired();
        }
    }
}
