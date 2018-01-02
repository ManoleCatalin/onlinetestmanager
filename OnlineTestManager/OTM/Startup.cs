using AutoMapper;
using Business.Repository;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OTM.Seeder;
using OTM.Services;
using OTM.UserContext;

namespace OTM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<DbSeeder>();
            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IGroupsRepository, GroupsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUserContext, UserContext.UserContext>();
            services.AddTransient<ITestTypesRepository, TestTypesRepository>();
			services.AddTransient<ITestsRepository, TestsRepository>();
            services.AddTransient<IExercisesRepository, ExercisesRepository>();
            services.AddTransient<IAnswersRepository, AnswersRepository>();
            services.AddTransient<ITestInstancesRepository, TestInstancesRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
        IHostingEnvironment env,
        DbSeeder dbSeeder,
        ITestTypesRepository testTypesRepository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                dbContext.Database.Migrate();

                dbSeeder.SeedAsync(app.ApplicationServices, 
                    roleManager, 
                    userManager,
                    testTypesRepository).Wait();
            }

           
        }
    }
}
