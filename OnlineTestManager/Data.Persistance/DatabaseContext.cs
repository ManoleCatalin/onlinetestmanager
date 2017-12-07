using System.Linq;
using Data.Core.Configuration;
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
            modelBuilder.AddConfiguration(new AnswerConfiguration());
            modelBuilder.AddConfiguration(new ExerciseConfiguration());
            modelBuilder.AddConfiguration(new FileConfiguration());
            modelBuilder.AddConfiguration(new GradeConfiguration());
            modelBuilder.AddConfiguration(new GroupConfiguration());
            modelBuilder.AddConfiguration(new TestConfiguration());
            modelBuilder.AddConfiguration(new TestInstanceConfiguration());
            modelBuilder.AddConfiguration(new TestTypeConfiguration());
            modelBuilder.AddConfiguration(new UserConfiguration());
            modelBuilder.AddConfiguration(new UserGroupConfiguration());
            modelBuilder.AddConfiguration(new UserTypeConfiguration());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
