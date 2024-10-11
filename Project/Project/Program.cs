using Project.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using Microsoft.Win32;
using Project.Models.Repositories;
using Project.Data;
using Project.Models.Interfaces;
using Project.Models.Entities;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IReturnRepository, ReturnRepository>();
builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();

builder.Services.Decorate<IProductRepository, ProductRepositoryDecorator>();
builder.Services.Decorate<IOrderRepository, OrderRepositoryDecorator>();
builder.Services.Decorate<ICategoryRepository, CategoryRepositoryDecorator>();
builder.Services.Decorate<ICartItemsRepository, CartItemsRepositoryDecorator>();
builder.Services.Decorate<IAdminRepository, AdminRepositoryDecorator>();
builder.Services.Decorate<IUserRepository, UserRepositoryDecorator>();
builder.Services.Decorate<IOrderItemRepository, OrderItemRepositoryDecorator>();
builder.Services.Decorate<IReturnRepository, ReturnRepositoryDecorator>();
builder.Services.Decorate<IProductReviewRepository, ProductReviewRepositoryDecorator>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}



else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();




//using Project.Data;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Project.Models.Interfaces;
//using Project.Models.Repositories;
//using Project.Models.Entities; // Make sure to include this if it's not already there

//var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//// Register the DbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

//// Register Identity with your custom User class
//builder.Services.AddIdentity<User, IdentityRole>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = true;
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders(); // This includes UserManager and RoleManager services

//// Add Razor Pages
//builder.Services.AddRazorPages();

//builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();
//builder.Services.AddScoped<IAdminRepository, AdminRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
//builder.Services.AddScoped<IReturnRepository, ReturnRepository>();
//builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();

//builder.Services.Decorate<IProductRepository, ProductRepositoryDecorator>();
//builder.Services.Decorate<IOrderRepository, OrderRepositoryDecorator>();
//builder.Services.Decorate<ICategoryRepository, CategoryRepositoryDecorator>();
//builder.Services.Decorate<ICartItemsRepository, CartItemsRepositoryDecorator>();
//builder.Services.Decorate<IAdminRepository, AdminRepositoryDecorator>();
//builder.Services.Decorate<IUserRepository, UserRepositoryDecorator>();
//builder.Services.Decorate<IOrderItemRepository, OrderItemRepositoryDecorator>();
//builder.Services.Decorate<IReturnRepository, ReturnRepositoryDecorator>();
//builder.Services.Decorate<IProductReviewRepository, ProductReviewRepositoryDecorator>();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//// Map Razor Pages and Controllers
//app.MapRazorPages();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
