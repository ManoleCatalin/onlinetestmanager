﻿using Data.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserConfiguration :DbEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(user => user.Id);
            entity.Property(user => user.FirstName).HasMaxLength(255).IsRequired();
            entity.Property(user => user.LastName).HasMaxLength(255).IsRequired();
            entity.Property(user => user.Email).HasMaxLength(255).IsRequired();
            entity.Property(user => user.PasswordHash).HasMaxLength(255).IsRequired();  
        }
    }
}
