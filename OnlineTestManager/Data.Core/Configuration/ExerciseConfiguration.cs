using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> entity)
        {
            entity.HasKey(exercise => exercise.Id);
            entity.Property(exercise => exercise.Description).HasMaxLength(255).IsRequired();
        }
    }
}
