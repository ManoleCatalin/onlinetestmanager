using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Test");

            builder.HasKey(test => test.Id);
            builder.Property(test => test.CreatedAt).IsRequired();
            builder.Property(test => test.Description).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
            builder.Property(test => test.Name).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();

            builder
                .HasMany(e => e.Exercises)
                .WithOne(e => e.Test)
                .HasForeignKey(p => p.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.TestInstances)
                .WithOne(x => x.Test)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
