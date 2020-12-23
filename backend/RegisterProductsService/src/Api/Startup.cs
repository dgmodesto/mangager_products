using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Api.ViewModels;
using AutoMapper;
using DI.Settings;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using Prometheus;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;

            var elasticUri = _configuration["ElasticConfiguration:Uri"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true
                })
                .CreateLogger();

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("EnvironmentName: {0}", _env.EnvironmentName);

            services.AddControllers();


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "register_products_service",
                        Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    }
                );
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, Product>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            ConsumerSettings.BuildDependencyInjection(services);


            services.AddDistributedRedisCache(options =>
            {
                options.Configuration =
                    Environment.GetEnvironmentVariable("REDIS_CONNECTION");
                options.InstanceName = "ApiRegisterProductService";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            // Custom Metrics to count requests for each endpoint and the method
            var counter = Metrics.CreateCounter("register_products_service", "Counts requests to the RegisterProductService API endpoints", new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });
            app.Use((context, next) => {
                counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
                return next();
            });
            // Use the Prometheus middleware
            app.UseMetricServer();
            app.UseHttpMetrics();
            app.UseHttpsRedirection();
            loggerFactory.AddSerilog();


            app.UseStaticFiles();

            // Use the CORS policy

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"));
        }
    }
}
