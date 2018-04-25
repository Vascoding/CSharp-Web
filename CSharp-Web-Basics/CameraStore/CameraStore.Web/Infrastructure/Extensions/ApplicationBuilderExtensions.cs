using CameraStore.Data;
using CameraStore.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CameraStore.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<CameraDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var roleName = "Administrator";
                        var roleExists = await roleManager.RoleExistsAsync(roleName);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = roleName
                            });
                        }

                        var adminUser = await userManager.FindByEmailAsync("admin@abv.bg");

                        var user = new User
                        {
                            Email = "admin@abv.bg",
                            UserName = "admin@abv.bg",
                            IsRestricted = false
                        };

                        if (adminUser == null)
                        {
                            await userManager.CreateAsync(user
                            , "123");

                            await userManager.AddToRoleAsync(user, roleName);
                        }
                    })
                    .Wait();
            }
            return app;
        }
    }
}
