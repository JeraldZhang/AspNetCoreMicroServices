using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LocationReporter.Events;
using LocationReporter.Services;
using LocationReporter.Converters;
using LocationReportService.Services;

namespace LocationReporter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddOptions();
            services.AddAMQPEventEmitter(Configuration);
            services.AddTeamServiceClien(Configuration);
            services.AddSingleton<ICommandEventConverter, CommandEventConverter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Asked for instances of singletons during Startup
            // to force initialization early.

            app.UseMvc();
        }
    }
}

