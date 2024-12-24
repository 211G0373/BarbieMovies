using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddDbContext<BarbieMovies.Models.BarbiemoviesContext>(
   optionsBuilder => optionsBuilder.UseMySql("server=localhost;user=root;database=barbiemovies;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"))
    );






var app = builder.Build();
app.UseStaticFiles();
app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapDefaultControllerRoute();



app.Run();
