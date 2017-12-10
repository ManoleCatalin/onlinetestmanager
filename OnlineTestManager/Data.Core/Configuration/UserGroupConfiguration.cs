using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(ug => new { ug.UserId, ug.GroupId });
            
            builder
                .HasOne(userGroup => userGroup.User)
                .WithMany(user => user.UserGroups)
                .HasForeignKey(userGroup => userGroup.UserId);

            builder
                .HasOne(userGroup => userGroup.Group)
                .WithMany(group => group.UserGroups)
                .HasForeignKey(userGroup => userGroup.GroupId);
        }
    }
}
