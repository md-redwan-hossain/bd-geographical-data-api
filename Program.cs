using System.Text.Json;
using System.Text.Json.Serialization;
using BdGeographicalData.Api.District;
using BdGeographicalData.Api.Division;
using BdGeographicalData.Api.SubDistrict;
using BdGeographicalData.Shared;
using BdGeographicalData.Shared.EnvironmentVariable;
using BdGeographicalData.Utils;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

const string secretsJson = "secrets.json";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile(secretsJson, optional: true, reloadOnChange: true);

IEnvVariableFactory envVariableFactory;

if (builder.Environment.IsDevelopment())
{
    envVariableFactory = new DevelopmentFactory(builder.Configuration);
    builder.Services.AddSingleton<IEnvVariableFactory, DevelopmentFactory>();
}
else
{
    envVariableFactory = new ProductionFactory(builder.Configuration);
    builder.Services.AddSingleton<IEnvVariableFactory, ProductionFactory>();
}


var envVariable = envVariableFactory.CreateOrGet();

builder.Services.AddDbContext<BdGeographicalDataDbContext>(opts =>
    opts.UseSqlite(envVariable.DatabaseUrl));

builder.Services.AddResponseCaching();

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.UseResponseCaching();

app.Use((context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(envVariable.ResponseCacheDurationInSecond)
        };

    var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();
    if (responseCachingFeature is not null)
        responseCachingFeature.VaryByQueryKeys = new[] { "*" };

    return next();
});

app.Run();