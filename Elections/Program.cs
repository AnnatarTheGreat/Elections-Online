using PresidentSite.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using PresidentSite.Models.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IBallot, Ballot>();
builder.Services.AddSingleton<IVoter, Voter>();
builder.Services.AddTransient<IRepository, Repository>();

string? connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<ElectorCounterContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "cookie";
            options.LoginPath = "/Authorization";
            options.AccessDeniedPath = "/Home/Privacy";
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
        });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("user"));
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }

