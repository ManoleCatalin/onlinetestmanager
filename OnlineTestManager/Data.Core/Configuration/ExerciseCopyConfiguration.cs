using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class ExerciseCopyConfiguration : IEntityTypeConfiguration<ExerciseCopy>
    {
        public void Configure(EntityTypeBuilder<ExerciseCopy> builder)
        {
            builder.ToTable("ExerciseCopy");

            builder.HasKey(exercise => exercise.Id);
            builder.Property(exercise => exercise.Description).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();

            builder
                .HasMany(c => c.AnswersCopies)
                .WithOne(e => e.ExerciseCopy)
                .HasForeignKey(p => p.ExerciseCopyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.TestInstance)
                .WithMany(x => x.ExerciseCopies)
                .HasForeignKey(x => x.TestInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
