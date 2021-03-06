﻿using System;

using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.NLogIntegration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TestTransit.Shared;

namespace TestQueryCommandPublisher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit();

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddScoped(
                provider => provider.GetRequiredService<IBus>().CreateRequestClient<IQueryCommand, IQueryCommandResult>(new Uri($"rabbitmq://localhost/{MessageQueue.QueryCommandService}"), TimeSpan.FromSeconds(15)));

            services.AddSingleton(
                provider => Bus.Factory.CreateUsingRabbitMq(
                    cfg =>
                        {
                            cfg.UseNLog();

                            var host = cfg.Host(
                                "localhost",
                                "/",
                                h =>
                                    {
                                        h.Username("rabbitmq");
                                        h.Password("rabbitmq");
                                    });
                        }));

            services.AddHostedService<BusService>();
        }
    }
}
