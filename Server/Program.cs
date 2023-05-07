using Microsoft.AspNetCore.Authentication.Negotiate;

using RTSADocs.Data.Services;
using RTSADocs.Data;
using RTSADocs.Server;
using RTSADocs.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?? throw new ArgumentNullException($"No connection string DefaultConnection has been configured in appsettings.json");
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddDataAccess(connectionString);
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();