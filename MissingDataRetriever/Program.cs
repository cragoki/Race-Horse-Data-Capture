﻿using Infrastructure.Config.IoC;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Stashbox;
using System;

namespace MissingDataRetriever
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            IConfiguration config;
            //Configure Logging
            host.ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Error);
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
                services.AddHostedService<MissingDataRetriever>();

                services.AddDbContextPool<DbContextData>(option =>
                    option.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(120)));
            });

            return host;

        }

    }
}