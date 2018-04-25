namespace Cats.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Cats.Handlers.Contracts;
    using Cats.Middleware;
    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DatabaseMigrationMiddleware>();
        }

        public static IApplicationBuilder UseHtmlContentType(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpContentTypeMiddleware>();
        }

        public static IApplicationBuilder UserRequestHandlers(this IApplicationBuilder builder)
        {
            var handlers = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(IHandler).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHandler>()
                .OrderBy(h => h.Order);

            foreach (var handler in handlers)
            {
                builder.MapWhen(handler.Contidion, app =>
                {
                    app.Run(handler.ReqiestHandler);
                });
            }

            return builder;
        }
    }
}