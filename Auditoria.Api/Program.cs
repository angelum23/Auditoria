using System.Text.Json.Serialization;
using Asp.Versioning;
using Auditoria.Api.Erros;
using Auditoria.Api.Extensions;
using Auditoria.Aplicacao;
using Auditoria.Aplicacao.Profiles;
using Auditoria.Dominio.Infra;
using Auditoria.Infra;
using Auditoria.Mongo;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddMongoDbContext(builder.Configuration)
    .AddInfra(builder.Configuration)
    .AddDomain()
    .AddApplication()
    .AddControllers()
    .AddJsonOptions(opt=> { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });


builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddMvc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger API Auditoria", Version = "v1" });
    c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}/AuditoriaDoc.xml");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(x => x.AddDefaultPolicy(option =>
    option.AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials()
));
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddAutoMapper(typeof(ApplicationAutoMapperProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider.GetRequiredService<AutoMapper.IConfigurationProvider>();
    provider.AssertConfigurationIsValid();
}

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.MapHealthChecks("/healthz")
    .AllowAnonymous();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();