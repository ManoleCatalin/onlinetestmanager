using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace OTM.Seeder
{
    public class DbSeeder
    {
        readonly ILogger _logger;

        public DbSeeder(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("CustomersDbSeederLogger");
        }

        public async Task SeedAsync(IServiceProvider serviceProvider,
            RoleManager<UserType> roleManager,
            UserManager<User> userManager)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = serviceScope.ServiceProvider.GetService<DatabaseContext>();

                if (!await service.UserTypes.AnyAsync())
                {
                    await InsertCustomersSampleData(service, roleManager, userManager);
                }

            }
        }

        public async Task InsertCustomersSampleData(DatabaseContext db,
            RoleManager<UserType> roleManager,
            UserManager<User> userManager)
        {
            var userTypes = GetUserTypes();
            foreach (var userType in userTypes)
            {
                if (!await roleManager.RoleExistsAsync(userType.Name))
                {
                    await roleManager.CreateAsync(userType);
                }
            }

            await db.SaveChangesAsync();
        }

        private static IEnumerable<UserType> GetUserTypes()
        {
            var student = UserType.Create("Student");
            var teacher = UserType.Create("Teacher");
            return new List<UserType> {student, teacher};
        }

    }
}