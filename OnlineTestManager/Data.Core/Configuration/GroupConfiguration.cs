using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(group => group.Id);
            builder.Property(group => group.CreatedAt).IsRequired();
            builder.Property(group => group.Description).HasMaxLength(255).IsRequired();
            builder.Property(group => group.Name).HasMaxLength(255).IsRequired();
        }
    }
}
