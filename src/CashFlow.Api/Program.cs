using System.Text;
using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;
using CashFlow.Api.Token;
using CashFlow.Application;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure;
using CashFlow.Infrastructure.DataAccess.Migrations;
using CashFlow.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddCors(setupAction: options =>
{
    options.AddPolicy(name: "Frontend", configurePolicy: policy =>
    {
        policy
            .WithOrigins("http://localhost:5295", "https://cash-flow-jgc.vercel.app")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction: config =>
{
    config.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });

    config.AddSecurityRequirement(securityRequirement: document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference(referenceId: "Bearer", hostDocument: document),
            []
        }
    });
});

builder.Services.AddMvc(setupAction: options => options.Filters.Add(filterType: typeof(ExceptionFilter)));

builder.Services.AddInfrastructure(configuration: builder.Configuration, connectionString: builder.Configuration.GetConnectionString(name: "connection")!);
builder.Services.AddApplication();

builder.Services.AddAuthentication(configureOptions: config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions: config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: builder.Configuration.GetValue<string>(key: "Settings:Jwt:SigningKey")!))
    };
});

builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

app.UseCors(policyName: "Frontend");

app.MapHealthChecks(pattern: "/Health", options: new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [key: HealthStatus.Healthy] = StatusCodes.Status200OK,
        [key: HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CultureMiddleware>();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(); 
// }

app.UseHttpsRedirection();
app.MapControllers();

if (!builder.Configuration.IsTestEnvironment())
{
    await MigrateDatabase();  
};

app.Run();

async Task MigrateDatabase()
{
    await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
    await DataBaseMigration.MigrateDatabase(serviceProvider: scope.ServiceProvider);
}
