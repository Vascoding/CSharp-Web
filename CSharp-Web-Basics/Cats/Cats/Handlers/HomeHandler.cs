namespace Cats.Handlers
{
    using System;
    using System.Linq;
    using Cats.Data;
    using Cats.Handlers.Contracts;
    using Cats.Infrastructure;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class HomeHandler : IHandler
    {
        public int Order => 1;

        public Func<HttpContext, bool> Contidion =>
            ctx => ctx.Request.Path.Value == "/" && ctx.Request.Method == HttpMethod.Get;
        public RequestDelegate ReqiestHandler => async (context) =>
        {
            var env = context.RequestServices.GetRequiredService<IHostingEnvironment>();

            await context.Response.WriteAsync($"<h1>{env.ApplicationName}</h1>");

            var db = context.RequestServices.GetRequiredService<CatsDbContext>();

            var catsData = db.Cats
                .Select(c => new
                {
                    c.Id,
                    c.Name
                })
                .ToList();

            await context.Response.WriteAsync("<ul>");

            foreach (var cat in catsData)
            {
                await context.Response.WriteAsync($@"<li><a href=""/cat/{cat.Id}"">{cat.Name}</a></li>");
            }

            await context.Response.WriteAsync("</ul>");

            await context.Response.WriteAsync(@"
                                                 <form action=""/cat/add"">
                                                    <input type=""submit"" value=""Add Cat"" />
                                                 </form>");
        };
    }
}