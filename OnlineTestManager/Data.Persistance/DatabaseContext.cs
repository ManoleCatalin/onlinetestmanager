using System.Linq;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestInstance> TestInstances { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(userGroup => userGroup.User)
                .WithMany(user => user.UserGroups)
                .HasForeignKey(userGroup => userGroup.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(userGroup => userGroup.Group)
                .WithMany(group => group.UserGroups)
                .HasForeignKey(userGroup => userGroup.GroupId);

            modelBuilder.Entity<Grade>()
                .HasKey(grade => new {grade.UserId, grade.TestInstanceId});

            modelBuilder.Entity<Grade>()
                .HasOne(grades => grades.User)
                .WithMany(user => user.Grades)
                .HasForeignKey(grades => grades.UserId);

            modelBuilder.Entity<Grade>()
                .HasOne(grades => grades.TestInstance)
                .WithMany(testInstace => testInstace.Grades)
                .HasForeignKey(grades => grades.TestInstanceId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
