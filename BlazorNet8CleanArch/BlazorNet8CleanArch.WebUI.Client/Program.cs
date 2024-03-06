using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BlazorNet8CleanArch.Application;
using BlazorNet8CleanArch.Infrastructure;
using BlazorNet8CleanArch.Infrastructure.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using MudExtensions.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddMudServices();
builder.Services.AddMudExtensions();

await builder.Build().RunAsync();
