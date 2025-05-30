using StatFinder.Repos;
using System.Data;
using Microsoft.Data.SqlClient;
using StatFinder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(s =>
{
    var db = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    db.Open();
    return db;
});

builder.Services.AddScoped<IPokemonRepo, PokemonRepo>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

builder.Services.AddHttpClient<PokeApiService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

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
