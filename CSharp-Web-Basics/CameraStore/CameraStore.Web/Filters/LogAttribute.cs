using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
namespace CameraStore.Web.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            using (var writer = new StreamWriter("logs.txt", true))
            {
                var date = DateTime.Now;
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                var username = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];

                var logMessage = $"{date} – {ipAddress} – {username} – {controller}.{action}";

                if (context.Exception != null)
                {
                    var exeptionType = context.Exception.GetType().Name;
                    var exeptionMessage = context.Exception.Message;
                    logMessage = $"[!] {logMessage} – {exeptionType} – {exeptionMessage}";
                }

                writer.WriteLine(logMessage);
            }
        }
    }
}
