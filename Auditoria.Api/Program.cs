using System.Text.Json.Serialization;
using Auditoria.Api.Autenticacao;
using Auditoria.Api.Erros;
using Auditoria.Api.Extensions;
using Auditoria.Aplicacao;
using Auditoria.Infra;
using Auditoria.Mongo;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// builder.Services.AddScoped<ApiKeyAuthenticationHandler>();
// builder.Services.AddAuthentication().AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme, null);

builder.Services.AddMongoDbContext(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfra(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(opt=> { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning().AddMvc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger API Pública Next Fit", Version = "v1" });
    c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}/AuditoriaDoc.xml");

    c.AddSecurityDefinition(AuthConsts.ApiKeyDefaultScheme, new OpenApiSecurityScheme
    {
        Description = "Api Key para acessar a API",
        Type = SecuritySchemeType.ApiKey,
        Name = AuthConsts.ApiKeyHeaderName,
        In = ParameterLocation.Header
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = AuthConsts.ApiKeyDefaultScheme
        },
        In = ParameterLocation.Header
    };

    var requeriment = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };
    
    c.AddSecurityRequirement(requeriment);

});

// Adiciona filtro de api key a nivel de controller ou endpoint
// Necessário adicionar [ServiceFilter(typeof(ApiKeyAuthFilter))] como atributo para "ativar" o filtro
// builder.Services.AddScoped<ApiKeyAuthFilter>();

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

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.MapHealthChecks("/healthz")
    .AllowAnonymous();

// Configure the HTTP request pipeline.

app.UseCors();

//Swagger habilitado em prod, pois é a documentação da api e possui autenticação
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization();

app.Run();