﻿using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class TestTypeConfiguration : IEntityTypeConfiguration<TestType>
    {
        public void Configure(EntityTypeBuilder<TestType> builder)
        {
            builder.ToTable("TestType");
            builder.HasKey(testType => testType.Id);
            builder.Property(testType => testType.Type).HasMaxLength(CoreConfigurationConstants.MaxLength).IsRequired();
        }
    }
}
