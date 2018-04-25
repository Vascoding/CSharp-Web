namespace Cats.Handlers.Contracts
{
    using System;
    using Microsoft.AspNetCore.Http;

    public interface IHandler
    {
        int Order { get; }

        Func<HttpContext, bool> Contidion { get; }

        RequestDelegate ReqiestHandler { get; }
    }
}