using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> entity)
        {
            entity.HasKey(group => group.Id);
            entity.Property(group => group.CreatedAt).IsRequired();
            entity.Property(group => group.Description).HasMaxLength(255).IsRequired();
            entity.Property(group => group.Name).HasMaxLength(255).IsRequired();
        }
    }
}
