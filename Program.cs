using Autofac.Extensions.DependencyInjection;
using Autofac;
using BdGeographicalData.Shared.AppSettings;
using BdGeographicalData.Shared;
using BdGeographicalData.Utils;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Async(wt => wt.Console())
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((_, loggerConfiguration) => loggerConfiguration
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(builder.Configuration)
    );

    builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

    var appSettingsData = new AppSettingsDataResolver(builder.Configuration).Resolve();

    builder.Services.AddDbContext<BdGeographicalDataDbContext>(opts =>
        opts.UseSqlite(appSettingsData.DatabaseUrl));

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new ApiModule());
    });

    builder.Services
        .AddControllers(opts =>
        {
            opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            opts.OutputFormatters.RemoveType<StringOutputFormatter>();
        })
        .AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
            opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

    builder.Services.AddResponseCaching();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthorization();
    app.MapControllers();

    app.UseResponseCaching();
    app.Use(new Middleware(appSettingsData).ResponseCache);

    app.Run();
    return 0;
}
catch (Exception e)
{
    Console.WriteLine(e);
    Log.Fatal(e, "Failed to start application");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}