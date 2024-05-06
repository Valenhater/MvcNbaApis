using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using MvcNbaApis.Data;
using MvcNbaApis.Helpers;
using MvcNbaApis.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient
    (builder.Configuration.GetSection("KeyVault"));
});

SecretClient secretClient = builder.Services.BuildServiceProvider().GetService<SecretClient>();
KeyVaultSecret secret = await secretClient.GetSecretAsync("SecretNba"); //Aqui ponemos el nombre del secret
string connectionString = secret.Value;

builder.Services.AddTransient<ServicePartidos>();
builder.Services.AddTransient<ServiceJugadores>();
builder.Services.AddTransient<ServiceEntradas>();
builder.Services.AddTransient<ServiceUsuarios>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<HelperPathProvider>();
builder.Services.AddSingleton<HelperTools>();
builder.Services.AddSingleton<HelperCryptography>();


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NbaContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Nba}/{action=PartidosJugados}/{id?}");

app.Run();
