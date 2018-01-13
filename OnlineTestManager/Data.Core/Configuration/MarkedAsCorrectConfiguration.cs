using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class MarkedAsCorrectConfiguration : IEntityTypeConfiguration<MarkedAsCorrect>
    {
        public void Configure(EntityTypeBuilder<MarkedAsCorrect> builder)
        {
            builder.ToTable("MarkedAsCorrect");

            builder.HasKey(x => new {AnswerId = x.AnswerId,
                ExerciseId = x.ExerciseId,
                TestInstanceId = x.TestInstanceId,
                UserId = x.UserId
            });

            builder.HasOne(x => x.ExerciseResponse)
                .WithMany(x => x.MarkedAsCorrects)
                .HasForeignKey(x => new { UserId = x.UserId, ExerciseId = x.ExerciseId, TestInstanceId = x.TestInstanceId });
        }
    }
}
