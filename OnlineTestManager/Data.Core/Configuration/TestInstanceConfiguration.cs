﻿using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class TestInstanceConfiguration : IEntityTypeConfiguration<TestInstance>
    {
        public void Configure(EntityTypeBuilder<TestInstance> entity)
        {
            entity.HasKey(testInstance => testInstance.Id);
            entity.Property(testInstance => testInstance.ConnectionToken).HasMaxLength(255);
            entity.Property(testInstance => testInstance.Duration).IsRequired();
        }
    }
}