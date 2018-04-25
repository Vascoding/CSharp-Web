namespace Cats.Handlers
{
    using System;
    using Cats.Data;
    using Cats.Data.Models;
    using Cats.Handlers.Contracts;
    using Cats.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class AddCatHandler : IHandler
    {
        public int Order => 2;
        public Func<HttpContext, bool> Contidion => ctx => ctx.Request.Path.Value == "/cat/add";

        public RequestDelegate ReqiestHandler => async (context) =>
        {
            if (context.Request.Method == HttpMethod.Get)
            {
                context.Response.StatusCode = 302;
                context.Response.Headers.Add("Location", "/add-cats.html");
            }
            else if (context.Request.Method == HttpMethod.Post)
            {
                var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                var formData = context.Request.Form;

                var cat = new Cat
                {
                    Name = formData["Name"],
                    Age = int.Parse(formData["Age"]),
                    Breed = formData["Breed"],
                    ImageUrl = formData["ImageUrl"]
                };

                db.Cats.Add(cat);

                try
                {
                    await db.SaveChangesAsync();
                    context.Response.Redirect("/");
                }
                catch
                {
                    await context.Response.WriteAsync("<h2>Invalid cat data!</h2>");
                    await context.Response.WriteAsync(@"<a href=""/cat/add"">Back to the form</a>");
                }
            }
        };
    }
}