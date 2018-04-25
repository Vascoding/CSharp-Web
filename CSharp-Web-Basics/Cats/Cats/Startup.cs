

namespace Cats
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Cats.Data;
    using Cats.Data.Models;
    using Cats.Infrastructure;
    using Cats.Infrastructure.Extensions;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
                options.UseSqlServer(@"Server=DESKTOP-924HS8U\SQLEXPRESS;Database=CatsDb;Integrated Security=True;"));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDatabaseMigration()
            .UseStaticFiles()
            .UseHtmlContentType()
            .UserRequestHandlers();
           
            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page Was Not Found!");
            });
        }
    }
}
