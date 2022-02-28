using Microsoft.AspNetCore.Builder;
using SharedNote.Application.Middleware;

namespace SharedNote.Application
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder AddApplicationBuilder(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestResponseLoggingMiddleware>();
            builder.UseMiddleware<ExceptionMiddleware>();
            return builder;
        }
    }
}
