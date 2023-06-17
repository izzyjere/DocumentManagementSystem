using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using MudBlazor.Services;

using RTSADocs;
using RTSADocs.Authentication;
using RTSADocs.Data;
using RTSADocs.Data.Services;
using RTSADocs.Services;
using RTSADocs.Shared.Services;

using SimpleAuthentication;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")??throw new ArgumentNullException("ConnectionString");
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHangfire(t =>
{
    t.UseSqlServerStorage(connectionString);   
});
builder.Services.AddHangfireServer();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSimpleAuthentication(userStoreOptions =>
{
    userStoreOptions.UseSqlServer(connectionString);
});
builder.Services.AddTransient<ICurrentUserService,CurrentUserService>();
builder.Services.AddDataAccess(connectionString);
builder.Services.AddScoped<IFileSystemService, LocalFileSystemService>();

builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthenticationStateProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MigrateDb();
app.UseSimpleAuthentication();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseHangfireDashboard();
app.InitFileStoreCleaner();
app.Run();