﻿using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answer");

            builder.HasKey(answer => answer.Id);
            builder.Property(answer => answer.Description).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
            builder.Property(answer => answer.Correct).IsRequired();

            builder
                .HasOne(e => e.Exercise)
                .WithMany(c => c.Answers)
                .HasForeignKey(x => x.ExerciseId);
        }
    }
}
