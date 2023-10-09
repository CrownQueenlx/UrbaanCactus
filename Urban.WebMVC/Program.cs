using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Urban.WebMVC.Data;
using Urban.Services.BoutiqueService;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// DbContext configuration, adds the DbContext for the dependency injection
var connectionString = builder.Configuration.GetConnectionString("UrbanCactus");
builder.Services.AddDbContext<UrbanCactusContext>(Options =>
{
    Options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IBoutiqueService, BoutiqueService>();
// builder.Services.AddScoped<IItemService, IItemService>();
// builder.Services.AddScoped<IProductService, ProductService>();
// builder.Services.AddScoped<IProductTypesService, ProductTypesService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
