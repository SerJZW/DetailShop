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



app.UseStaticFiles();

app.Run();
