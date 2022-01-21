using Microsoft.AspNetCore.Builder;
using SharedNote.Application.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder AddApplicationBuilder(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CustomExceptionMiddleware>();
            return builder;
        }
    }
}
