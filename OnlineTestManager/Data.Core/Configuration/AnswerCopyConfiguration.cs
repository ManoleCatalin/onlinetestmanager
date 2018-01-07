using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class AnswerCopyConfiguration : IEntityTypeConfiguration<AnswerCopy>
    {
        public void Configure(EntityTypeBuilder<AnswerCopy> builder)
        {
            builder.ToTable("AnswerCopy");

            builder.HasKey(answer => answer.Id);
            builder.Property(answer => answer.Description).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
            builder.Property(answer => answer.Correct).IsRequired();

            builder
                .HasOne(e => e.ExerciseCopy)
                .WithMany(c => c.AnswersCopies)
                .HasForeignKey(x => x.ExerciseCopyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
