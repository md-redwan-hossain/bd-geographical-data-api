using System.Text.Json.Serialization;
using BdRegionalData.Api.SubDistrict;
using BdRegionalData.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

if (Helper.GetDbConnectionString(builder) is null)
    builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

var envVars = new EnvVariable()
{
    DatabaseUrl = Helper.GetDbConnectionString(builder),
};

new EnvVariableValidator().ValidateAndThrow(envVars);

builder.Services.AddDbContext<BdRegionalDataDbContext>(options =>
    options.UseSqlite(envVars.DatabaseUrl));

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