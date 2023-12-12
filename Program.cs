using System.Text.Json;
using System.Text.Json.Serialization;
using BdGeographicalData.Api.District;
using BdGeographicalData.Api.Division;
using BdGeographicalData.Api.SubDistrict;
using BdGeographicalData.Shared;
using BdGeographicalData.Utils;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

var envVars = new EnvVariableFactory(builder.Configuration).CreateOrGet();

if (envVars.UseSecretsJson is 1)
    builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<BdGeographicalDataDbContext>(opts =>
    opts.UseSqlite(envVars.DatabaseUrl));

builder.Services.AddResponseCaching();

builder.Services.AddSingleton<IEnvVariableFactory, EnvVariableFactory>();
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
            MaxAge = TimeSpan.FromSeconds(envVars.ResponseCacheDurationInSecond)
        };
    
    var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();
    if (responseCachingFeature is not null)
        responseCachingFeature.VaryByQueryKeys = new[] { "*" };
    
    return next();
});

app.Run();