using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(exercise => exercise.Id);
            builder.Property(exercise => exercise.Description).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();

            builder
                .HasMany(c => c.Answers)
                .WithOne(e => e.Exercise)
                .HasForeignKey(p => p.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
