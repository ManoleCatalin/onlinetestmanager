using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> entity)
        {
            entity.HasKey(file => file.Id);
            entity.Property(file => file.CreatedAt).IsRequired();
            entity.Property(file => file.Path).HasMaxLength(255).IsRequired();
            entity.Property(file => file.Url).HasMaxLength(255).IsRequired();   
        }
    }
}
