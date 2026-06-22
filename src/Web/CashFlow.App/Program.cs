using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CashFlow.App;
using CashFlow.App.Auth;
using CashFlow.Adapter.Services;
using Microsoft.AspNetCore.Components.Authorization;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args: args);
builder.RootComponents.Add<App>(selector: "#app");
builder.RootComponents.Add<HeadOutlet>(selector: "head::after");

string apiBaseUrl = builder.Configuration[key: "ApiBaseUrl"] ?? "http://localhost:5280/";
Uri apiBaseUri = new(uriString: apiBaseUrl);

builder.Services.AddScoped(implementationFactory: _ => new HttpClient { BaseAddress = apiBaseUri });

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<CookieAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(implementationFactory: serviceProvider => serviceProvider.GetRequiredService<CookieAuthenticationStateProvider>());
builder.Services.AddScoped<UserApiService>();
builder.Services.AddScoped<LoginApiService>();
builder.Services.AddScoped<ExpenseApiService>();
builder.Services.AddScoped<CategoryApiService>();
builder.Services.AddScoped<GoalApiService>();
builder.Services.AddScoped<IncomeApiService>();

await builder.Build().RunAsync();
