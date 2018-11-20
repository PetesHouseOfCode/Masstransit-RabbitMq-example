﻿using GreenPipes;

using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using TestTransit.Shared;

namespace TestTransitConsume2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<PublishMyMessageConsumer>();

            services.AddMassTransit(x => { x.AddConsumer<PublishMyMessageConsumer>(); });

            services.AddSingleton(
                provider => Bus.Factory.CreateUsingRabbitMq(
                    cfg =>
                        {
                            var host = cfg.Host(
                                "localhost",
                                "/",
                                h =>
                                    {
                                        h.Username("rabbitmq");
                                        h.Password("rabbitmq");
                                    });

                            cfg.ReceiveEndpoint(
                                host,
                                MessageQueue.TrackerService,
                                e =>
                                    {
                                        e.PrefetchCount = 1;
                                        // e.UseMessageRetry(x => x.Interval(2, 500));

                                        e.LoadFrom(provider);

                                        EndpointConvention.Map<IPublishMyMessage>(e.InputAddress);
                                    });
                        }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddScoped(
                provider => provider.GetRequiredService<IBus>().CreateRequestClient<IPublishMyMessage>());

            services.AddSingleton<IHostedService, BusService>();
            services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));
        }
    }
}
