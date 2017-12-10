using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.Property(user => user.FirstName).HasMaxLength(255).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(255).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(255).IsRequired();
            builder.Property(user => user.PasswordHash).HasMaxLength(255).IsRequired();  
        }
    }
}
