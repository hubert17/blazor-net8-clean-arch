using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BlazorNet8CleanArch.Application;
using BlazorNet8CleanArch.Infrastructure;
using BlazorNet8CleanArch.Infrastructure.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
