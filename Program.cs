using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;
using ProductManagementSystem.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<IproductRepository, ProductRepository>();

builder.Services.AddScoped<IproductRepository, DBProductRepository>(); // To work with the Database
builder.Services.AddScoped<IFileUpload, FileUpload>();

//builder.Services.AddDbContext<ProductContext>(options => options.UseSqlite("Data Source=FreshMarket.db"));
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer("Server=GWTN156-11\\SQLEXPRESS;Database=FreshMarket;TrustServerCertificate=True; Trusted_Connection=true;MultipleActiveResultSets=True"));
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer());
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<UserContext>();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer("Server=GWTN156-11\\SQLEXPRESS;Database=FreshMarketUsers;TrustServerCertificate=True; Trusted_Connection=true;MultipleActiveResultSets=True"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Custom error page
}

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<ProductContext>();
    dbcontext.Database.EnsureCreated(); // Creates the database
    var userdbcontext = scope.ServiceProvider.GetRequiredService<UserContext>();
    userdbcontext.Database.EnsureCreated();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
