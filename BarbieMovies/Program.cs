using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddDbContext<BarbieMovies.Models.BarbiemoviesContext>(
   optionsBuilder => optionsBuilder.UseMySql("server=localhost;user=root;database=barbiemovies;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"))
    );




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccesDenied";
    }
    );





var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();



app.UseStaticFiles();
app.MapAreaControllerRoute(
    name: "areas",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapDefaultControllerRoute();



app.Run();
