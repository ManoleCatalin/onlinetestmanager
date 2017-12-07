using Data.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class ExerciseConfiguration : DbEntityConfiguration<Exercise>
    {
        public override void Configure(EntityTypeBuilder<Exercise> entity)
        {
            entity.HasKey(exercise => exercise.Id);
            entity.Property(exercise => exercise.Description).HasMaxLength(255).IsRequired();
        }
    }
}
