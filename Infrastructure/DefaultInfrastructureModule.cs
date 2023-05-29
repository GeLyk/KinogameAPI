using Module = Autofac.Module;

namespace Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            RegisterContext<AppDbContext>(builder);

            builder.RegisterGeneric(typeof(EfRepository<>))
                    .As(typeof(IRepository<>))
                    .InstancePerLifetimeScope();

            //builder.RegisterType<EventBus>()
            //    .As<IEventBus>()
            //    .InstancePerLifetimeScope();


            //builder.RegisterType<MediatorHandler>()
            //    .As<IMediatorHandler>();


            //builder.RegisterType<CacheService>()
            //   .As<ICacheService>()
            //   .SingleInstance();

            //builder.RegisterType<SystemLockHandling>()
            //    .As<ISystemLockHandling>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<MatchResultsSettlementService>()
            //    .As<IMatchResultsSettlementService>();

            //builder.RegisterType<EventHistoryService>()
            //    .As<IEventHistoryService>();

            //builder
            //    .RegisterType<InfrastructureMetrics>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            //builder
            //    .RegisterGeneric(typeof(BackgroundConcurrentQueue<>))
            //    .As(typeof(IBackgroundConcurrentQueue<>))
            //    .SingleInstance();


            //builder.Register(componentContext =>
            //{
            //    var serviceProvider = componentContext.Resolve<IServiceProvider>();
            //    var configuration = componentContext.Resolve<IConfiguration>();

            //    return new SqlDistributedSynchronizationProvider(configuration.GetConnectionString("DefaultConnection"));
            //})
            //.As<IDistributedLockProvider>().
            //SingleInstance();

            //builder
            //    .RegisterDecorator(typeof(HttpRepositoryMetricsDecorator), typeof(IHttpRepository));
            //builder
            //    .RegisterGenericDecorator(typeof(EfRepositoryMetricsDecorator<>), typeof(IRepository<>));

            //builder.RegisterType<ServiceInitiationHealthCheck>()
            //    .AsSelf()
            //    .SingleInstance();

            //builder
            //    .RegisterType<MetricsCacheService>()
            //    .As(typeof(IMetricsCacheService))
            //    .SingleInstance()
            //    .OnActivating(async e =>
            //    {
            //        await e.Instance.Init();
            //    });

            //builder
            //    .RegisterType<EventPhasesCacheService>()
            //    .As(typeof(IEventPhasesCacheService))
            //    .SingleInstance()
            //    .OnActivating(async e =>
            //    {
            //        await e.Instance.Init();
            //    });
        }

        public void RegisterContext<TContext>(ContainerBuilder builder)
            where TContext : DbContext
        {
            builder.Register(componentContext =>
            {
                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<AppDbContext>(new Dictionary<Type, IDbContextOptionsExtension>());

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        serverOptions =>
                            serverOptions.EnableRetryOnFailure(5,
                            TimeSpan.FromSeconds(30),
                            null));

                return optionsBuilder.Options;
            })
            .As<DbContextOptions<TContext>>()
            .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
