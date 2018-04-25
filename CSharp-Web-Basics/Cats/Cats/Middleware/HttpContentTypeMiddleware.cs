namespace Cats.Middleware
{
    using System.Threading.Tasks;
    using Cats.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class HttpContentTypeMiddleware
    {
        private readonly RequestDelegate next;

        public HttpContentTypeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Content-Type", "text/html");

            return this.next(context);
        }
    }
}