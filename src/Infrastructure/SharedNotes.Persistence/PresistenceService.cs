using Microsoft.Extensions.DependencyInjection;
using SharedNote.Application.Interfaces.Common;
using SharedNotes.Persistence.Context;
using SharedNotes.Persistence.Repositories.BaseRepositories;


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
