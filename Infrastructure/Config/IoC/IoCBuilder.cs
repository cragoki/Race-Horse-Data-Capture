﻿
using Core.Algorithms;
using Core.Interfaces.Algorithms;
using Core.Interfaces.Data;
using Core.Interfaces.Data.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.PunterAdmin.Services;
using Microsoft.Extensions.Configuration;
using Stashbox;

namespace Infrastructure.Config.IoC
{
    public static class IocBuilder
    {
        public static IStashboxContainer Container { get; private set; }

        private static void Initialize(IStashboxContainer container)
        {
            Container = container;
        }

        public static IStashboxContainer AddDependencies(this IStashboxContainer container, IConfiguration configuration)
        {
            Initialize(container);

            //container.Configure(config => config.WithDefaultLifetime(Lifetimes.Transient).WithDefaultValueInjection());

            //container.RegisterSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Repository
            container.RegisterScoped<IEventRepository, EventRepository>();
            container.RegisterScoped<IHorseRepository, HorseRepository>();
            container.RegisterScoped<IConfigurationRepository, ConfigurationRepository>();
            container.RegisterScoped<IAlgorithmRepository, AlgorithmRepository>();
            container.RegisterScoped<IMappingTableRepository, MappingTableRepository>();
            container.RegisterSingleton<IDbContextData, DbContextData>();

            //Algorithms
            container.RegisterScoped<ITopSpeedOnly, TopSpeedOnly>();
            container.RegisterScoped<ITsRPR, TsRPR>();
            container.RegisterScoped<IFormAlgorithm, FormAlgorithm>();
            container.RegisterScoped<IFormRevamped, FormRevamped>();
            container.RegisterScoped<IBentnersModel, BentnersModel>();
            container.RegisterScoped<IMyModel, MyModel>();

            //Punter Admin
            container.RegisterScoped<IAdminAlgorithmService, AdminAlgorithmService>();
            container.RegisterScoped<IAdminRaceService, AdminRaceService>();


            container.RegisterSettings(configuration);
            container.RegisterServices();
            container.RegisterJobs();
            container.RegisterHttpClients();

            return container;
        }

        public static IStashboxContainer RegisterSettings(this IStashboxContainer container, IConfiguration configuration)
        {
            //container.RegisterSectionSettings<IPaytahSettings, PaytahSettings>(configuration, SettingsConstants.PaytahSettings);

            return container;
        }

        public static IStashboxContainer RegisterServices(this IStashboxContainer container)
        {
            container.RegisterScoped<IEventService, EventService>();
            container.RegisterScoped<IRaceService, RaceService>();
            container.RegisterScoped<IScraperService, ScraperService>();
            container.RegisterScoped<IConfigurationService, ConfigurationService>();
            container.RegisterScoped<IMailService, MailService>();
            container.RegisterScoped<IAlgorithmService, AlgorithmService>();
            container.RegisterScoped<IAdjusterService, AdjusterService>();

            return container;
        }

        public static IStashboxContainer RegisterJobs(this IStashboxContainer container)
        {
            return container;
        }

        public static IStashboxContainer RegisterHttpClients(this IStashboxContainer container)
        {
            //container.RegisterInstance<IFactory<HttpClient>>(
            //    new Factory<HttpClient>(() => new HttpClient(new HttpClientHandler { UseCookies = false })));

            //container.RegisterScoped<IEmailClient, EmailClient>();


            return container;
        }
    }
}
