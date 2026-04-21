using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HouseRentingSystem.Middlewares
{
    public sealed class StopwatchMiddleware
    {
        private readonly RequestDelegate _next;

        public StopwatchMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            await _next(context);

            sw.Stop();

            var elapsedMs = sw.Elapsed.TotalMilliseconds.ToString("F2");

            if (!context.Response.HasStarted)
            {
                context.Response.Headers["X-Response-Time-ms"] = elapsedMs;
            }

            Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode} {elapsedMs} ms");
        }
    }

    public static class StopwatchMiddlewareExtensions
    {
        public static IApplicationBuilder UseStopwatch(this IApplicationBuilder app) =>
            app.UseMiddleware<StopwatchMiddleware>();
    }
}
