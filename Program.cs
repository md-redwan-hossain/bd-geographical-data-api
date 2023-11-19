using System.Text.Json.Serialization;
using BdGeographicalData.Api.District;
using BdGeographicalData.Api.Division;
using BdGeographicalData.Api.SubDistrict;
using BdGeographicalData.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var envVars = new EnvVariable()
{
    DatabaseUrl = builder.Configuration.GetConnectionString("DATABASE_URL"),
    UseSecretsJson = builder.Configuration.GetValue<bool>("USE_SECRETS_JSON")
};

new EnvVariableValidator().ValidateAndThrow(envVars);

if (envVars.UseSecretsJson)
    builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);


builder.Services.AddDbContext<BdGeographicalDataDbContext>(options =>
    options.UseSqlite(envVars.DatabaseUrl));

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
        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();