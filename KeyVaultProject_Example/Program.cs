

using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

#region Key Vault Config

var keyVoultUri = new Uri("Your Key Vault URL");

var TendandId = "Your App Registration Tendand ID";

var ClientId = "Your App Registration Client ID";

var ClientSecrets = "Your App Registration Client Secrets";

var credentials = new ClientSecretCredential(tenantId: TendandId, clientId: ClientId, clientSecret: ClientSecrets);

var secretClient = new SecretClient(keyVoultUri, credentials);

builder.Configuration.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


