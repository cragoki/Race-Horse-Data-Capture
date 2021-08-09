using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Stashbox;
using Infrastructure.Config.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace RHDCAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //_logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/article-hit", "POST");
            //_logger.LogTrace("Added new ArticleHit entity with Id {id}", entity.Id);
            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) 
        {
            var host = Host.CreateDefaultBuilder(args);

            //Configure Logging
            host.ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();
                // Enable NLog as one of the Logging Provider
                logging.AddNLog();
            });

            host.UseStashbox();

            //Using Stashbox for Dependancies
            host.ConfigureContainer<IStashboxContainer>((context, container) =>
            {
                container.AddDependencies(context.Configuration);
            });

            //Configure startup class
            host.ConfigureServices((hostContext, services) =>
            {
                services.Configure<HostOptions>(opts
                    => opts.ShutdownTimeout = TimeSpan.FromMinutes(2));
                services.AddHostedService<RHDCFetchAutomator>();

            });




            return host;

        }

    }
}
