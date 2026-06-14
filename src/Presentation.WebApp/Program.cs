using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Identity;

using Presentation.WebApp.DependencyInjections.Application;
using Presentation.WebApp.DependencyInjections.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection")!,
    builder.Environment
);
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

var app = builder.Build();

// ------------------- PIPELINE -------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    GymClassSeeder.Seed(db);
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    GymClassSeeder.Seed(db);

    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<ApplicationUser>>();

    // Roles

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    if (!await roleManager.RoleExistsAsync("Member"))
        await roleManager.CreateAsync(new IdentityRole("Member"));

    // Admin user

    const string adminEmail = "admin@test.com";
    const string adminPassword = "Administrator@1";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser is null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            FirstName = "System",
            LastName = "Administrator"
        };

        var createResult = await userManager.CreateAsync(
            adminUser,
            adminPassword);

        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
    else if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.UseHttpsRedirection();

// ✅ MUST come early
app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/404");
app.UseRouting();

// 🔐 REQUIRED for Identity (important even if you don’t use it yet)
app.UseAuthentication();
app.UseAuthorization();

// MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();