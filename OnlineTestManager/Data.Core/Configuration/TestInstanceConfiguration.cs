using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class TestInstanceConfiguration : IEntityTypeConfiguration<TestInstance>
    {
        public void Configure(EntityTypeBuilder<TestInstance> builder)
        {
            builder.ToTable("TestInstance");

            builder.HasKey(testInstance => testInstance.Id);
            builder.Property(testInstance => testInstance.Duration).IsRequired();
        }
    }
}
