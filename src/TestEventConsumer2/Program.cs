using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using GenericHosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NLog.Extensions.Hosting;

namespace TestEventConsume2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            NLog.LogManager.LoadConfiguration("NLog.config");
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            // Create a generic host builder which will serve as our IOC container
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                    {
                        configApp.SetBasePath(Directory.GetCurrentDirectory());
                        configApp.AddJsonFile("appsettings.json", true, true);
                        configApp.AddJsonFile(
                            $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                            optional: true);
                        //configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                        //configApp.AddCommandLine(args);
                    })
                .ConfigureServices((hostContext, services) => new Startup(hostContext.Configuration).ConfigureServices(services))
                .ConfigureServices(services => services.AddHostedService<MessageConsumerService>())
                .UseNLog();

            if (isService)
            {
                await builder.RunAsServiceAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }
    }
}
