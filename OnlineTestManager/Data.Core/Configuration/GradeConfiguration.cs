using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grade");

            builder.HasKey(grade => new { grade.UserId, grade.TestInstanceId });
            builder.Property(grade => grade.MarkedAt).IsRequired();
            builder.Property(grade => grade.Value).IsRequired();
            
            builder
                .HasOne(grades => grades.User)
                .WithMany(user => user.Grades)
                .HasForeignKey(grades => grades.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(grades => grades.TestInstance)
                .WithMany(testInstace => testInstace.Grades)
                .HasForeignKey(grades => grades.TestInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
