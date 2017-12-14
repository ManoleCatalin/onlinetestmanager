using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasKey(test => test.Id);
            builder.Property(test => test.CreatedAt).IsRequired();
            builder.Property(test => test.Description).HasMaxLength(Constants.MaxLength).IsRequired();
            builder.Property(test => test.Name).HasMaxLength(Constants.MaxLength).IsRequired();
        }
    }
}
