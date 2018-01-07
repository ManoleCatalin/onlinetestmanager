﻿using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class MarkedAsCorrectConfiguration : IEntityTypeConfiguration<MarkedAsCorrect>
    {
        public void Configure(EntityTypeBuilder<MarkedAsCorrect> builder)
        {
            builder.ToTable("MarkedAsCorrect");

            builder.HasKey(x => new {x.AnswerCopyId,
                x.ExerciseCopyId,
                x.TestInstanceId,
                x.UserId
            });

            builder.HasOne(x => x.ExerciseResponse)
                .WithMany(x => x.MarkedAsCorrects)
                .HasForeignKey(x => new {x.ExerciseResponseId, x.AnswerCopyId, x.UserId});
        }
    }
}