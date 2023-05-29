


using Autofac.Core;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Logging.ClearProviders();
   

    builder.Services.AddControllers(x =>
    {
        x.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
    }).AddNewtonsoftJson(x =>
    {
        x.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects;
    });

    //builder.Services.AddDefaultServices();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
        builder.RegisterModule(new DefaultApplicationModule()));

    builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
        builder.RegisterModule(new DefaultInfrastructureModule()));

    builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
        builder.RegisterModule(new DefaultApiModule())); 

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularOrigins",
        builder =>
        {
            builder.WithOrigins(
                                "http://localhost:4200"
                                )
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
    });

    var app = builder.Build();

    app.UseCors("AllowAngularOrigins");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

