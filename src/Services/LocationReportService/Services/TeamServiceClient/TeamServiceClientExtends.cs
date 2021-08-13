using LocationReporter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocationReportService.Services
{
    public static class TeamServiceClientExtends
    {
        public static IServiceCollection AddTeamServiceClien(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TeamServiceOptions>(configuration.GetSection("teamservice"));
            services.AddSingleton<ITeamServiceClient, HttpTeamServiceClient>();
            return services;
        }
    }
}
