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
            builder.Property(testInstance => testInstance.TestDescription).IsRequired()
                   .HasMaxLength(Constants.CoreConfigurationConstants.MaxLength);
            builder.Property(testInstance => testInstance.TestName).IsRequired()
                   .HasMaxLength(Constants.CoreConfigurationConstants.MaxLength);

            builder.HasOne(x => x.GroupCopy)
                .WithOne(x => x.TestInstance)
                .HasForeignKey<GroupCopy>(x => x.TestInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
