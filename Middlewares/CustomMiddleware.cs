using HouseRentingSystem.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HouseRentingSystem.Middlewares
{
    public class CustomMiddleware
    {
        private RequestDelegate next;
        public CustomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, HouseRentingSystemDbContext ctx, IConfiguration config) //IConfiguration
        {
            var housesCount = await ctx.Houses.CountAsync();
            Console.WriteLine();
            await this.next(httpContext);
        }
    }
}