using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using LocationReporter.Models;
using System;
using LocationReporter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace LocationReporter.Services
{
    public static class AMQPEventEmitterExtends
    {
        public static IServiceCollection AddAMQPEventEmitter(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AMQPOptions>(configuration.GetSection("amqp"));
            services.AddSingleton(typeof(IEventEmitter), typeof(AMQPEventEmitter));
            return services;
        }
    }
}