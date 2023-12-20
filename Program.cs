using System.Text.Json;
using System.Text.Json.Serialization;
using BdGeographicalData.Api.District;
using BdGeographicalData.Api.Division;
using BdGeographicalData.Api.SubDistrict;
using BdGeographicalData.Shared;
using BdGeographicalData.Utils;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Async(wt => wt.Console())
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);


    builder.Configuration.AddJsonFile(Constants.SecretsJsonFileName, optional: true, reloadOnChange: true);

    builder.Host.UseSerilog((_, loggerConfiguration) => loggerConfiguration
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Async(x => x.Console(
            restrictedToMinimumLevel: LogEventLevel.Information,
            outputTemplate: Constants.SerilogConsoleOutputTemplate
        ))
        .WriteTo.Async(x => x.AsyncRollingFile(
            restrictedToMinimumLevel: LogEventLevel.Error,
            pathFormat: Constants.SerilogFilePath
        ))
    );

    builder.Services.AddOptions<AppOptions>()
        .BindConfiguration(Constants.AppOptions)
        .ValidateDataAnnotations()
        .ValidateOnStart();

    var section = builder.Configuration.GetSection(Constants.AppOptions);
    var options = new AppOptions
    {
        DatabaseUrl = section.GetValue<string>("DatabaseUrl"),
        ResponseCacheDurationInSecond = section.GetValue<int>("ResponseCacheDurationInSecond")
    };

    builder.Services.AddDbContext<BdGeographicalDataDbContext>(opts =>
        opts.UseSqlite(options.DatabaseUrl));

    builder.Services.AddScoped<ResponseCacheConfigMiddleware>();
    builder.Services.AddScoped<IDivisionService, DivisionService>();
    builder.Services.AddScoped<IDistrictService, DistrictService>();
    builder.Services.AddScoped<ISubDistrictService, SubDistrictService>();


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
    app.UseMiddleware<ResponseCacheConfigMiddleware>();

    app.Run();
    return 0;
}

catch (HostAbortedException)
{
    Log.Information("HostAbortedException skipped");
    return 0;
}

catch (OptionsValidationException)
{
    return 1;
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