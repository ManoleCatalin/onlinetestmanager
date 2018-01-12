using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);

            builder
                .HasOne(a => a.UserRole)
                .WithOne(b => b.User)
                .HasForeignKey<UserRole>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.Property(user => user.FirstName).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
            builder.Property(user => user.PasswordHash).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();  
        }
    }
}
