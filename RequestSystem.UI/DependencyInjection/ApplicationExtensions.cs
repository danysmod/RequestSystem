using App.Services;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace RequestSystem.UI.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddAppExtensions(this IServiceCollection services)
        {
            services.AddScoped<IStatementService, StatementService>();

            return services;
        }
    }
}
