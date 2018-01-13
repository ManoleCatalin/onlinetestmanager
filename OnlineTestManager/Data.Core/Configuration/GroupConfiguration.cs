using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");

            builder.HasKey(group => group.Id);
            builder.Property(group => group.CreatedAt).IsRequired();
            builder.Property(group => group.Description).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
            builder.Property(group => group.Name).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();

            builder
                .HasMany(x => x.TestInstances)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
