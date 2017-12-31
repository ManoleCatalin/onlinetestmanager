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
                    await InsertRolesData(service, roleManager);
                    await InsertUsersSampleData(service, userManager);
                }
            }
        }

        public async Task InsertRolesData(DatabaseContext db,
            RoleManager<Role> roleManager)
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

        public async Task InsertUsersSampleData(DatabaseContext db,
            UserManager<User> userManager)
        {
            var roleNames = RoleConstants.GetRoleNames();
            const int userCount = 5;
            if (await db.Users.AnyAsync())
                return;
            foreach (var roleName in roleNames)
            {
                for (int i = 0; i < userCount; i++)
                {
                    var username = string.Format(roleName + "{0}", i);
                    var user = User.Create(username, username, username, username + "@gmail.com", username);
                    var result = await userManager.CreateAsync(user, user.PasswordHash);
                    if (!result.Succeeded) throw new Exception(string.Format("Failed creating user with username {0} ", username));
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }

            await db.SaveChangesAsync();
        }
    }
}