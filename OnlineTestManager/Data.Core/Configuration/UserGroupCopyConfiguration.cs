using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserGroupCopyConfiguration : IEntityTypeConfiguration<UserGroupCopy>
    {
        public void Configure(EntityTypeBuilder<UserGroupCopy> builder)
        {
            builder.ToTable("UserGroupCopy");

            builder.HasKey(ug => new { ug.UserId, ug.GroupCopyId });

            builder
                .HasOne(userGroup => userGroup.User)
                .WithMany(user => user.UserGroupCopies)
                .HasForeignKey(userGroup => userGroup.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(userGroup => userGroup.GroupCopy)
                .WithMany(group => group.UserGroupsCopies)
                .HasForeignKey(userGroup => userGroup.GroupCopyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
