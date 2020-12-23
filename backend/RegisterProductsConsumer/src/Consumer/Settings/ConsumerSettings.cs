using Application.Builder;
using Application.Services.Products;
using Application.Settings;
using Consumer.JobServices;
using Data.MySqlDB.ProductTable;
using Data.Repositories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Settings
{
    public static class ConsumerSettings
    {

        public static IServiceCollection BuildDependencyInjection(this IServiceCollection services)
        {
            services.AddJobs();
            services.AddApplicationService();
            services.AddDbContext();
            services.AddConsoleLogs();
            return services;
        }


        public static IServiceCollection AddJobs(this IServiceCollection services)
        {
            services.AddScoped<IJobService, JobService>();
            return services;
        }

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddSingleton(x => new KafkaSettings
            {
                GroupId = "PRODUCT-CONSUMER-GROUP",
                BootstrapServer = "localhost:9092",
                Topic = "PRODUCT-TOPIC"
            });

            services.AddScoped<IProductApplicationService, ProductApplicationService>();
            services.AddScoped<IKafkaBuilder, KafkaBuilder>();


            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {

            var connection = "server=products-db.cpkayivoztp6.sa-east-1.rds.amazonaws.com;user=admin;password=a1b2c3d4e5;database=ProductTestDb";

            services.AddDbContext<ProductContext>(options =>
            options.UseMySql(connection));

            services.AddScoped<IProductRepository, ProductRepository>();


            return services;
        }

        public static IServiceCollection AddConsoleLogs(this IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole());

            return services;
        }
    }
}
