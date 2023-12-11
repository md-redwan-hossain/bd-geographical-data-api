using System.Text.Json;
using BdGeographicalData.Api.District;
using BdGeographicalData.Api.Division;
using BdGeographicalData.Api.SubDistrict;
using BdGeographicalData.Shared;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

var envVars = new EnvVariable()
{
    DatabaseUrl = builder.Configuration.GetConnectionString("DATABASE_URL"),
    UseSecretsJson = builder.Configuration.GetValue<int>("USE_SECRETS_JSON")
};

new EnvVariableValidator().ValidateAndThrow(envVars);

if (envVars.UseSecretsJson == 1)
    builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);


builder.Services.AddDbContext<BdGeographicalDataDbContext>(opts =>
    opts.UseSqlite(envVars.DatabaseUrl));


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
app.Run();