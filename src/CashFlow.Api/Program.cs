using System.Text;
using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;
using CashFlow.Application;
using CashFlow.Infrastructure;
using CashFlow.Infrastructure.DataAccess.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

WebApplication app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

app.UseMiddleware<CultureMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
{
    await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
    await DataBaseMigration.MigrateDatabase(serviceProvider: scope.ServiceProvider);
}