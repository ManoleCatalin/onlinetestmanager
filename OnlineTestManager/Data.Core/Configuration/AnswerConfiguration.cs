using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> entity)
        {
            entity.HasKey(answer => answer.Id);
            entity.Property(answer => answer.Description).HasMaxLength(255).IsRequired();
            entity.Property(answer => answer.Correct).IsRequired();
        }
    }
}
