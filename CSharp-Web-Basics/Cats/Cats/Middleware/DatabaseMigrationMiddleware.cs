﻿namespace Cats.Middleware
{
    using System.Threading.Tasks;
    using Cats.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DatabaseMigrationMiddleware
    {
        private readonly RequestDelegate next;

        public DatabaseMigrationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();

            return this.next(context);
        }
    }
}