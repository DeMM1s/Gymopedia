using Gymopedia.Domain.Repositories;
using Gymopedia.Data;
using Gymopedia.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gymopedia.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //const string ConnectionName = "LocalDbConnection";
            //var connectionString = configuration.GetConnectionString(ConnectionName);
            var connectionString = "Host=127.0.0.1;Username=test;Password=test;Database=test;";

            services.AddDbContext<LocalDbContext>(dbContextOptionsBuilder =>
            dbContextOptionsBuilder.UseNpgsql(connectionString));
            
            

            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ICoachRepository, CoachRepository>();
            services.AddTransient<ICoachWorkDayRepository, CoachWorkDayRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<IClientToSessionRepository, ClientToSessionRepository>();
            services.AddTransient<IClientToCoachRepository, ClientToCoachRepository>();

            //services.AddScoped(typeof(IClientRepository), typeof(ClientRepository));

            return services;
        }
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

    }
}
