using CashFlow.API.Filters;
using CashFlow.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();

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
