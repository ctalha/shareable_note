﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedNote.Application.Interfaces.Common;
using SharedNote.Domain.Entites;
using SharedNotes.Persistence.Context;
using SharedNotes.Persistence.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SharedNotes.Persistence
{
    public static class PresistenceService
    {

        public static void AddPressitenceService(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.EnableSensitiveDataLogging();
            });


            //dependency registers
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
