using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Management.Endpoint;
using Steeltoe.Extensions.Logging;
using Steeltoe.Messaging.RabbitMQ.Host;

namespace BluePaw.Administration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            RabbitMQHost.CreateDefaultBuilder(args)
                .AddConfigServer(LoggerFactory.Create(builder => builder.AddConsole()))
                .AddHealthActuator()
                .AddInfoActuator()
                .AddLoggersActuator()
                .AddDynamicLogging()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
