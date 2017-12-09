using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> entity)
        {
            entity.HasKey(grade => new { grade.UserId, grade.TestInstanceId });
            entity.Property(grade => grade.MarkedAt).IsRequired();
            entity.Property(grade => grade.Value).IsRequired();
            
            entity
                .HasOne(grades => grades.User)
                .WithMany(user => user.Grades)
                .HasForeignKey(grades => grades.UserId);

            entity
                .HasOne(grades => grades.TestInstance)
                .WithMany(testInstace => testInstace.Grades)
                .HasForeignKey(grades => grades.TestInstanceId);
        }
    }
}
