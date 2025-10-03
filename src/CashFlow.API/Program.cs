using CashFlow.API.Filters;
using CashFlow.API.Middleware;
using CashFlow.Application;
using CashFlow.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddRouting(configureOptions: option => option.LowercaseUrls = true);
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(setupAction: options => options.Filters.Add(filterType: typeof(ExceptionFilter)));

builder.Services.AddInfrastructure(configuration: builder.Configuration);
builder.Services.AddApplication();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<CultureMIddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
