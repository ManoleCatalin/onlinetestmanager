using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class GroupCopyConfiguration : IEntityTypeConfiguration<GroupCopy>
    {
        public void Configure(EntityTypeBuilder<GroupCopy> builder)
        {
            builder.ToTable("GroupCopy");

            builder.HasKey(groupCopy => groupCopy.Id);
            builder.Property(groupCopy => groupCopy.Description).HasMaxLength(CoreConfigurationConstants.MaxLength)
                .IsRequired();
            builder.Property(groupCopy => groupCopy.Name).HasMaxLength(CoreConfigurationConstants.MaxLength)
                .IsRequired();

            builder.HasOne(x => x.TestInstance)
                .WithOne(x => x.GroupCopy)
                .HasForeignKey<TestInstance>(x => x.GroupCopyId)
                .OnDelete(DeleteBehavior.Cascade);
            //SetNull 

            builder
                .HasMany(x => x.UserGroupsCopies)
                .WithOne(x => x.GroupCopy)
                .HasForeignKey(x => x.GroupCopyId)
                .OnDelete(DeleteBehavior.Cascade);
            //SetNull 
        }
    }
}
