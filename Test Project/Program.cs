using System.Data;
using Microsoft.Data.SqlClient;
using StatFinder.Repos;
using StatFinder.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

// DB connection per request (scoped). Let Dapper open/close as needed.
builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<ICurrentTeamRepo, CurrentTeamRepo>();
builder.Services.AddScoped<IPokemonRepo, PokemonRepo>();

// PokéAPI service (typed client via HttpClientFactory)
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
