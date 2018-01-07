using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("File");

            builder.HasKey(file => file.Id);
            builder.Property(file => file.CreatedAt).IsRequired();
            builder.Property(file => file.Path).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
            builder.Property(file => file.Url).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();   
        }
    }
}
