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

            builder.HasKey(x => new {AnswerCopyId = x.AnswerId,
                ExerciseCopyId = x.ExerciseId,
                x.TestInstanceId,
                x.UserId
            });

            builder.HasOne(x => x.ExerciseResponse)
                .WithMany(x => x.MarkedAsCorrects)
                .HasForeignKey(x => new {x.ExerciseResponseId, AnswerCopyId = x.AnswerId, x.UserId});
        }
    }
}
