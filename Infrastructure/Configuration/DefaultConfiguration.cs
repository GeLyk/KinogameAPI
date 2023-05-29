namespace Infrastructure.Configuration
{
    public static class DefaultConfiguration
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //services.AddHostedService<HealthChecksHostedService>();
            //services.AddHostedService<NLogMetricsHostedService>();
            //services.AddHostedService<DatabaseSizeCollectorHostedService>();

            return services;
        }
    }
}
