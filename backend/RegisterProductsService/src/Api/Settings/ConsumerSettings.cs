
using Application.Services;
using Data.MySqlDB.ProductTable;
using Data.Repositories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
            services.AddApplicationService();
            services.AddRepositories();

            return services;
        }


        public static IServiceCollection AddConsumerService(this IServiceCollection services)
        {
            services.AddSingleton(x => new KafkaSettings
            {
                GroupId = Environment.GetEnvironmentVariable("KAFKA_GROUP_ID"),
                BootstrapServer = Environment.GetEnvironmentVariable("KAFKA_SERVER"),
                Topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC")
            });

            services.AddScoped<IConsumerService, ConsumerService>();
            services.AddScoped<IKafkaBuilder, KafkaBuilder>();

            return services;
        }

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IProductApplicationService, ProductApplicationService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            var connection = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
            services.AddDbContext<ProductContext>(options =>
            options.UseMySql(connection));

            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }

        
    }
}
