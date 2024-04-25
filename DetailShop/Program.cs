using DetailShop.App_Data;
using DetailShop.Models.DbModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMvc();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentication/Enter";
        options.LogoutPath = "/Aunthetication/Enter";
        options.AccessDeniedPath = "/Aunthetication/AccsessDenied";
    });



builder.Services.AddAuthorization();

string? connection = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();


var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{Action=Index}");

app.UseAuthentication();
app.UseAuthorization();

 async Task ConfigureRoles(IServiceProvider serviceProvider)
{
    // Создание ролей
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Присвоение ролей пользователю (например, администратору)
    var userManager = serviceProvider.GetRequiredService<UserManager<Account>>();
    var user = await userManager.FindByEmailAsync("admin@example.com");
    if (user != null)
    {
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.UseStaticFiles();

app.Run();
