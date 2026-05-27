using Shortly.Application.Interfaces;
using Shortly.Application.Services;
using Shortly.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shortly.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILinkService, LinkService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AppDbContext") ??
        throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

builder.Host.UseSerilog((hostingContext, services, configuration) =>
{
    configuration.ReadFrom.Configuration(hostingContext.Configuration);
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
    var log = logFactory.CreateLogger<DbInitializer>();

    log.LogDebug("Initializing ..");
    DbInitializer.Initialize(dbContext, log);
    log.LogDebug("Initializing ok.");
}

app.Run();