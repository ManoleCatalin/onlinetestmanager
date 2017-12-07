using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> entity)
        {
            entity.HasKey(userType => userType.Id);
            entity.Property(userType => userType.Type).HasMaxLength(255).IsRequired();
        }
    }
}
