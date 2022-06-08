using Microsoft.EntityFrameworkCore;
using musicStoreMVC.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<MusicStoreEntities>();
//builder.Services.AddScoped<MusicStoreSeeder>();
var connectionString = builder.Configuration.GetConnectionString("MusicStoreEntitiesDataBase");
builder.Services.AddTransient<MusicStoreSeeder>();
builder.Services.AddDbContext<MusicStoreEntities>(x=>x.UseSqlServer(builder.Configuration.GetConnectionString("MusicStoreEntitiesDataBase")));
var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<MusicStoreSeeder>();
        service.Seed();
    }
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MusicStoreEntities>();
    db.Database.Migrate();
}
app.Run();
