using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cooking.middleware
{
    public class UserMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello from Middelware 1 \n");
            await next(context);
            await context.Response.WriteAsync("Hello from Middelware 2 \n");
        }
    }
}
