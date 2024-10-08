﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;
using Stashbox;
using Infrastructure.Config.IoC;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AlgorithmAccuracyCalculator
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
                services.AddHostedService<AlgorithmAccuracyCalculator>();

                services.AddDbContextPool<DbContextData>(option =>
                    option.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.CommandTimeout(120)));
            });

            return host;

        }
    }
}
