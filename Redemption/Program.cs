    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
using Redemption.Infrastructure;
using Redemption.Interfaces;
using Redemption.Models.Data;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DbConnection");
    builder.Services.AddDbContext<RedemptionContext>(options => options.UseNpgsql(connection)).AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<RedemptionContext>();
    builder.Services.Configure<IdentityOptions>(options =>
    {
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    });

    builder.Services.ConfigureApplicationCookie(options =>
    {
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    options.LoginPath = "/Accounts/Login";
    options.AccessDeniedPath = "/Accounts/Login";
    options.SlidingExpiration = true;
    });
    builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IRedemptionService, RedemptionService>();
// Add services to the container.
    builder.Services.AddControllersWithViews();
var app = builder.Build();
    var scope = app.Services.CreateScope();
    try
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await AdminInitializer.SeedAdminUser(rolesManager, userManager);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }


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
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}");

    app.Run();
