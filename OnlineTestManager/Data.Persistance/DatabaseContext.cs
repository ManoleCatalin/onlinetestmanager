using System;
using System.Linq;
using Data.Core.Configuration;
using Data.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public sealed class DatabaseContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<Data.Core.Domain.Group> Groups { get; set; }
        public DbSet<Data.Core.Domain.UserGroup> UserGroups { get; set; }
        public DbSet<Data.Core.Domain.TestType> TestTypes { get; set; }
        public DbSet<Data.Core.Domain.Test> Tests { get; set; }
        public DbSet<Data.Core.Domain.TestInstance> TestInstances { get; set; }
        public DbSet<Data.Core.Domain.File> Files { get; set; }
        public DbSet<Data.Core.Domain.Grade> Grades { get; set; }
        public DbSet<Data.Core.Domain.Exercise> Exercises { get; set; }
        public DbSet<Data.Core.Domain.Answer> Answers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.ApplyConfiguration(new AnswerConfiguration());
            builder.ApplyConfiguration(new ExerciseConfiguration());
            builder.ApplyConfiguration(new FileConfiguration());
            builder.ApplyConfiguration(new GradeConfiguration());
            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new TestConfiguration());
            builder.ApplyConfiguration(new TestInstanceConfiguration());
            builder.ApplyConfiguration(new TestTypeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserGroupConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
