
using Microsoft.Extensions.DependencyInjection;
using Producer.Builder;
using Producer.Services.Products;
using Producer.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace DI.Settings
{
    public static class ConsumerSettings
    {

        public static IServiceCollection BuildDependencyInjection(this IServiceCollection services)
        {
            services.AddConsumerService();

            return services;
        }


        public static IServiceCollection AddConsumerService(this IServiceCollection services)
        {
            services.AddSingleton(x => new KafkaSettings
            {
                GroupId = Environment.GetEnvironmentVariable("KAFKA-GROUP-ID"),
                BootstrapServer = Environment.GetEnvironmentVariable("KAFKA-SERVER"),
                Topic = Environment.GetEnvironmentVariable("KAFKA-TOPIC")
            });

            services.AddScoped<IConsumerService, ConsumerService>();
            services.AddScoped<IKafkaBuilder, KafkaBuilder>();

            return services;
        }
    }
}
