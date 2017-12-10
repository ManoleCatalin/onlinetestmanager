using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(answer => answer.Id);
            builder.Property(answer => answer.Description).HasMaxLength(255).IsRequired();
            builder.Property(answer => answer.Correct).IsRequired();
        }
    }
}
