using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedNote.Application.Caching;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Application.Security;
using System.Reflection;

namespace SharedNote.Application
{
    public static class ApplicationServices
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);
            services.AddSingleton<ICacheManager, InMemoryCacheManager>();
            services.AddSingleton<ITokenHelper, TokenHelper>();
        }
    }
}

