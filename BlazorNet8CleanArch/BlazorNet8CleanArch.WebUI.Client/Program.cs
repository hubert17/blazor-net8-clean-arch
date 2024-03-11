using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BlazorNet8CleanArch.Application;
using BlazorNet8CleanArch.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using MudExtensions.Services;
using BlazorNet8CleanArch.Infrastructure.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddMudServices();
builder.Services.AddMudExtensions();

await builder.Build().RunAsync();
