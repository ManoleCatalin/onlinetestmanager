using Data.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserTypeConfiguration : DbEntityConfiguration<UserType>
    {
        public override void Configure(EntityTypeBuilder<UserType> entity)
        {
            entity.HasKey(userType => userType.Id);
            entity.Property(userType => userType.Type).HasMaxLength(255).IsRequired();
        }
    }
}
