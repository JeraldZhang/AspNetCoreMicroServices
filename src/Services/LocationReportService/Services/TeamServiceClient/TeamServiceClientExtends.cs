using LocationReporter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationReportService.Services
{
    public static class TeamServiceClientExtends
    {
        public static IServiceCollection AddTeamServiceClien(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TeamServiceOptions>(configuration.GetSection("teamservice"));
            services.AddSingleton(typeof(ITeamServiceClient), typeof(HttpTeamServiceClient));
            return services;
        }
    }
}
