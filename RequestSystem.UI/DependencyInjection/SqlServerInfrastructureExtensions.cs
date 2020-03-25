using Domain.Services;
using Domain.Statements;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RequestSystem.UI.DependencyInjection
{
    public static class SqlServerInfrastructureExtensions
    {
        public static IServiceCollection AddSQLServerPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IStatementFactory, EntityFactory>();
            
            services.AddDbContext<DataContext>(
                 options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IStatementRepository, StatementRepository>();

            return services;
        }
    }
}
