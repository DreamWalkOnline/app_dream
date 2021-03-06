﻿using System;
using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Dream.Space.Cache;
using Dream.Space.Calculators;
using Dream.Space.Calculators.IndicatorProcessor;
using Dream.Space.Data;
using Dream.Space.Data.Azure;
using Dream.Space.Data.Repositories;
using Dream.Space.Data.Repositories.Accounts;
using Dream.Space.Data.Services;
using Dream.Space.Infrastructure.Loggers;
using Dream.Space.Infrastructure.Processors;
using Dream.Space.Infrastructure.Processors.GlobalIndicators;
using Dream.Space.Infrastructure.Settings;
using Dream.Space.Jobs;
using Dream.Space.Models.Calculators;
using Dream.Space.Playground;
using Dream.Space.Reader;
using Dream.Space.Reader.Validators;
using Dream.Space.Stock;
using Dream.Space.Stock.Yahoo.Client;
using Dream.Space.Infrastructure.Processors.CompanyQuotes;
using Dream.Space.Stock.Nasdaq.Client;

namespace Dream.Space.Infrastructure.IoC
{
    public class IoCContainer
    {
        private static IoCContainer _instance;
        private IContainer _container;

        public void Register()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        public IContainer Container => _container;

        private IoCContainer()
        {

        }

        public void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<AppSettingsWrapper>().As<IAppSettings>();

            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerDependency();
            builder.RegisterType<ArticleRepository>().As<IArticleRepository>().InstancePerDependency();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerDependency();
            builder.RegisterType<SectionRepository>().As<ISectionRepository>().InstancePerDependency();
            builder.RegisterType<IndicatorRepository>().As<IIndicatorRepository>().InstancePerDependency();
            builder.RegisterType<StrategyRepository>().As<IStrategyRepository>().InstancePerDependency();
            builder.RegisterType<CompanyIndicatorRepository>().As<ICompanyIndicatorRepository>().InstancePerDependency();
            builder.RegisterType<RuleRepository>().As<IRuleRepository>().InstancePerDependency();
            builder.RegisterType<RuleSetRepository>().As<IRuleSetRepository>().InstancePerDependency();
            builder.RegisterType<StrategyRuleSetRepository>().As<IStrategyRuleSetRepository>().InstancePerDependency();
            builder.RegisterType<CompanyRuleSetRepository>().As<ICompanyRuleSetRepository>().InstancePerDependency();
            builder.RegisterType<RuleSetDetailsRepository>().As<IRuleSetDetailsRepository>().InstancePerDependency();
            builder.RegisterType<VRuleSetRepository>().As<IVRuleSetRepository>().InstancePerDependency();
            builder.RegisterType<VStrategyRuleSetRepository>().As<IVStrategyRuleSetRepository>().InstancePerDependency();
            builder.RegisterType<VStrategyRuleRepository>().As<IVStrategyRuleRepository>().InstancePerDependency();
            builder.RegisterType<ScheduledJobRepository>().As<IScheduledJobRepository>().InstancePerDependency();
            builder.RegisterType<ScheduledJobDetailsRepository>().As<IScheduledJobDetailsRepository>().InstancePerDependency();
            builder.RegisterType<IndicatorIntermediateResultsRepository>().As<IIndicatorIntermediateResultsRepository>().InstancePerDependency();
            builder.RegisterType<ProcessorLogRepository>().As<IProcessorLogRepository>().InstancePerDependency();
            builder.RegisterType<ChartLayoutRepository>().As<IChartLayoutRepository>().InstancePerDependency();
            builder.RegisterType<ChartIndicatorRepository>().As<IChartIndicatorRepository>().InstancePerDependency();
            builder.RegisterType<ChartPlotRepository>().As<IChartPlotRepository>().InstancePerDependency();
            builder.RegisterType<JournalRepository>().As<IJournalRepository>().InstancePerDependency();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerDependency();
            builder.RegisterType<AccountTradeRepository>().As<IAccountTradeRepository>().InstancePerDependency();
            builder.RegisterType<AccountTransferRepository>().As<IAccountTransferRepository>().InstancePerDependency();

            builder.RegisterType<DreamDbContext>().InstancePerDependency();

            builder.RegisterType<JobService>().As<IJobService>();
            builder.RegisterType<ScheduledJobsService>().As<IScheduledJobsService>();
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<RuleService>().As<IRuleService>();
            builder.RegisterType<StrategyService>().As<IStrategyService>();
            builder.RegisterType<ChartLayoutService>().As<IChartLayoutService>();
            builder.Register(c => new StorageAccountConfiguration
            {
                AccountName = ConfigurationManager.AppSettings["AzureStorageAccountName"],
                ConnectionString = ConfigurationManager.AppSettings["AzureStorageConnection"]
            }).SingleInstance();

            builder.RegisterType<AzureStorageClient>().As<IStorageClient>();
            builder.RegisterType<ArticleStorageService>().As<IArticleStorageService>();
            builder.RegisterType<IndicatorService>().As<IIndicatorService>();
            builder.RegisterType<RuleSetService>().As<IRuleSetService>();
            builder.RegisterType<PlaygroundService>().As<IPlaygroundService>();
            builder.RegisterType<CompanyManagerService>().As<ICompanyService>();
            builder.RegisterType<JournalService>().As<IJournalService>();
            builder.RegisterType<AccountService>().As<IAccountService>();

            builder.RegisterType<FileReaderConfiguration>().SingleInstance();
            builder.RegisterType<CalculatorFactory>().SingleInstance();
            builder.RegisterType<DataCache>().As<IDataCache>().SingleInstance();
            builder.RegisterType<PlaygroundProcessor>();
            builder.RegisterType<PlaygroundConfigurationLoader>();

            builder.Register(c => new YahooFinanceClientConfig() { Proxy = "" }).SingleInstance();
            builder.Register(c => new NasdaqStockClientConfig() { Proxy = "" }).SingleInstance();
            builder.RegisterType<NasdaqStockClient>().As<IMarketStockClient>();
            //builder.RegisterType<YahooFinanceClient>().As<IMarketStockClient>();
            builder.RegisterType<QuotesFileReader>().As<IQuotesFileReader>().InstancePerDependency();
            builder.RegisterType<FileReaderValidator>().As<IFileReaderValidator>().InstancePerDependency();

            builder.RegisterType<CompanySectorRepository>().As<ICompanySectorRepository>().InstancePerDependency();
            builder.RegisterType<GlobalIndicatorRepository>().As<IGlobalIndicatorRepository>().InstancePerDependency();
            builder.RegisterType<GlobalIndicatorService>().As<IGlobalIndicatorService>().InstancePerDependency();
            builder.RegisterType<ProcessorLogService>().As<IProcessorLogService>().InstancePerDependency();

            
            //Calculators
            builder.RegisterType<EMACalculator>().As<IIndicatorCalculator>();
            builder.RegisterType<ForceIndexCalculator>().As<IIndicatorCalculator>();
            builder.RegisterType<MACDCalculator>().As<IIndicatorCalculator>();
            builder.RegisterType<ImpulseSystemCalculator>().As<IIndicatorCalculator>();
            builder.RegisterType<NHNLCalculator>().As<IIndicatorCalculator>();
            builder.RegisterType<SMACalculator>().As<IIndicatorCalculator>();
            builder.RegisterType<RSICalculator>().As<IIndicatorCalculator>();


            //Jobs
            builder.RegisterType<CompanyImportJob>().As<IJob>().As<ICompanyImportJob>();
            builder.RegisterType<QuotesImportJob>().As<IJob>().As<IQuotesImportJob>();
            builder.RegisterType<SP500CompanyImportJob>().As<IJob>().As<ISP500CompanyImportJob>();

            //Loggers
            builder.RegisterType<ProcessorLogger>().As<IProcessorLogger>();

            //Processors
            builder.RegisterType<GlobalIndicatorsProcessor>().As<IProcessor>();
            builder.RegisterType<CompanyQuotesProcessor>().As<IProcessor>();
            builder.RegisterType<CompanySP500QuotesProcessor>().As<IProcessor>();
            builder.RegisterType<IndicesQuotesProcessor>().As<IProcessor>();

            builder.Register(c =>
            {
                var seconds = int.Parse(ConfigurationManager.AppSettings["GlobalIndicatorsProcessorIntervalInSeconds"]);
                return new GlobalIndicatorsProcessorConfig
                {
                    Interval = new TimeSpan(0, 0, seconds)
                };

            }).SingleInstance();

            builder.Register(c =>
            {
                var seconds = int.Parse(ConfigurationManager.AppSettings["CompanyQuotesProcessorIntervalInSeconds"]);
                return new CompanyQuotesProcessorConfig
                {
                    Interval = new TimeSpan(0, 0, seconds)
                };

            }).SingleInstance();

            builder.Register(c =>
            {
                var seconds = int.Parse(ConfigurationManager.AppSettings["CompanyQuotesProcessorIntervalInSeconds"]);
                return new CompanySP500QuotesProcessorConfig
                {
                    Interval = new TimeSpan(0, 0, seconds)
                };

            }).SingleInstance();

            builder.Register(c =>
            {
                var seconds = int.Parse(ConfigurationManager.AppSettings["CompanyQuotesProcessorIntervalInSeconds"]);
                return new IndicesQuotesProcessorConfig
                {
                    Interval = new TimeSpan(0, 0, seconds)
                };

            }).SingleInstance();

        }

        public static IoCContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IoCContainer();
                }
                return _instance;
            }
        }
    }
}