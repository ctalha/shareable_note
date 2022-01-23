using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Application.Caching;

namespace SharedNote.Application
{
    // öRNEK SERVİCES REGISTERION İŞLEMİ
    public static class ApplicationServices
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);
            services.AddSingleton<ICacheManager, InMemoryCacheManager>();
        }
    }
}

