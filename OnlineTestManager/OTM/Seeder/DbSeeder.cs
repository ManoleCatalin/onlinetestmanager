using System;
using System.Threading.Tasks;
using Constants;
using Data.Core.Domain;
using Data.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace OTM.Seeder
{
    public class DbSeeder
    {
        public DbSeeder()
        {
        }

        public async Task SeedAsync(IServiceProvider serviceProvider,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = serviceScope.ServiceProvider.GetService<DatabaseContext>();

                if (!await service.Roles.AnyAsync())
                {
                    await InsertCustomersSampleData(service, roleManager, userManager);
                }
            }
        }

        public async Task InsertCustomersSampleData(DatabaseContext db,
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            var roleNames = RoleConstants.GetRoleNames();
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(Role.Create(roleName));
                }
            }

            await db.SaveChangesAsync();
        }
    }
}