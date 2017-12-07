﻿using Data.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserGroupConfiguration : DbEntityConfiguration<UserGroup>
    {
        public override void Configure(EntityTypeBuilder<UserGroup> entity)
        {
            entity.HasKey(ug => new { ug.UserId, ug.GroupId });
            
            entity
                .HasOne(userGroup => userGroup.User)
                .WithMany(user => user.UserGroups)
                .HasForeignKey(userGroup => userGroup.UserId);

            entity
                .HasOne(userGroup => userGroup.Group)
                .WithMany(group => group.UserGroups)
                .HasForeignKey(userGroup => userGroup.GroupId);
        }
    }
}
