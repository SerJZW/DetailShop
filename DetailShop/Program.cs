using DetailShop.App_Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMvc();
string? connection = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();
app.MapControllerRoute
(
    name: "default",
    pattern: "{Controller=Home}/{Action=Index}"
);

app.UseStaticFiles();

//app.MapGet("/", (ApplicationContext db) => db.Orders.ToList());



app.Run();
