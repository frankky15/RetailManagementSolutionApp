using Extensions.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RMS.Controllers;
using RMS.Data;
using RMS.Models;
using RMS.Repository;
using RMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

//Repository
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IStaffRepository, StaffRepository>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<IStoreRepository, StoreRepository>();

//Service
builder.Services.AddTransient<IProductionService, ProductionService>();
builder.Services.AddTransient<ISalesService, SalesService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
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

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


using (var scope = app.Services.CreateScope()) // Seed Roles
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "StoreManager", "ProductManager", "StockManager", "Cashier" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope()) // Whitelist Admins
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string email = "admin1@admin.com";
    string password = "Admin1.";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new ApplicationUser()
        {
            UserName = email,
            Email = email
        };

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }

    var adminsWhitelist = new[] { email };

    foreach (var admin in adminsWhitelist)
    {
        var user = await userManager.FindByEmailAsync(admin);

        if (user == null)
        {
            Console.WriteLine($"Error: There was a problem while trying to whitelist admin account, email '{admin}' was not found in the db.");
            continue;
        }

        if (!await userManager.IsInRoleAsync(user, "Admin"))
            await userManager.AddToRoleAsync(user, "Admin");
    }
}

// using (var scope = app.Services.CreateScope()) // This is to fix an issue with the seeding of the stock...
// {
//     var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

//     var productswithbadstock = dataContext.Products.Where(p => p.Stocks.Count() < dataContext.Stores.Count()).ToList();

//     var stores = dataContext.Stores.ToList();

//     foreach (var product in productswithbadstock)
//     {
//         foreach (var store in stores)
//         {
//             var stock = new Stock
//             {
//                 StoreId = store.StoreId,
//                 ProductId = product.ProductId,
//                 Quantity = 0
//             };

//             try
//             {
//                 dataContext.Set<Stock>().Add(stock);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine("Error: " + ex);
//             }
//         }

//         try
//         {
//             dataContext.SaveChanges();
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Error: " + ex);
//         }
//     }
// }


app.Run();
