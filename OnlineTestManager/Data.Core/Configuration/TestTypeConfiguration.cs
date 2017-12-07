using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class TestTypeConfiguration : IEntityTypeConfiguration<TestType>
    {
        public void Configure(EntityTypeBuilder<TestType> entity)
        {
            entity.HasKey(testType => testType.Id);
            entity.Property(testType => testType.Type).HasMaxLength(255).IsRequired();
        }
    }
}
