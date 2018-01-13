using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class ExerciseResponseConfiguration : IEntityTypeConfiguration<ExerciseResponse>
    {
        public void Configure(EntityTypeBuilder<ExerciseResponse> builder)
        {
            builder.ToTable("ExerciseResponse");

            builder.HasKey(x => new {UserId = x.UserId, ExerciseId = x.ExerciseId, TestInstanceId = x.TestInstanceId});

            builder.HasOne(x => x.User)
                .WithMany(x => x.ExerciseResponses)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Exercise)
                .WithMany(x => x.ExerciseResponses)
                .HasForeignKey(x => x.ExerciseId);

            //builder.HasOne(x => x.TestInstance)
            //    .WithMany(x => x.ExerciseResponses)
            //    .HasForeignKey(x => x.TestInstanceId);
        }
    }
}
