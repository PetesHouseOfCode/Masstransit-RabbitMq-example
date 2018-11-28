using System;

using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.NLogIntegration;
using MassTransit.Util;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TestTransit.Shared;

namespace TestCommandPublisher
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

                            EndpointConvention.Map<ICommand>(host.Address.AppendToPath(MessageQueue.CommandService));
                        }));

            services.AddHostedService<BusService>();
        }
    }
}
