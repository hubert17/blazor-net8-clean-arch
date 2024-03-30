using BlazorNet8CleanArch.WebUI.Components;
using BlazorNet8CleanArch.Application;
using BlazorNet8CleanArch.Infrastructure;
using MudBlazor.Services;
using MudExtensions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorNet8CleanArch.Infrastructure.Authentication;
using SmartComponents.Inference.OpenAI;
using BlazorNet8CleanArch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BlazorNet8CleanArch.Infrastructure.Handlers.ProductHandler;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddCascadingAuthenticationState();
//builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    //options.DefaultScheme = IdentityConstants.ApplicationScheme;
    //options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
//.AddIdentityCookies();
.AddCookie();

builder.Services.AddSmartComponents()
    .WithInferenceBackend<OpenAIInferenceBackend>();

builder.Services.AddMudServices();
builder.Services.AddMudExtensions();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductListHandler).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorNet8CleanArch.WebUI.Client._Imports).Assembly);

app.MapControllers();
app.MapGet("/api/hello", () => "Hello, World!");

app.Run();

public partial class Program { }
