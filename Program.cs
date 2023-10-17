using Cinema.Context;
using Cinema.Interfaces;
using Cinema.Repositories;
using Cinema.Service.Implementations;
using Cinema.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register ApplicationDbContext
builder.Services.AddDbContext<CinemaDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("CinemaDb"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
});
builder.Services.AddRazorPages();

builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IOriginCountryRepository, OriginCountryRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
builder.Services.AddScoped<IHallRepository, HallRepository>();
builder.Services.AddScoped<IRestrictionRepository, RestrictionRepository>();
builder.Services.AddScoped<IFilmGenreRepository, FilmGenreRepository>();
builder.Services.AddScoped<IFilmCountryRepository, FilmCountryRepository>();
builder.Services.AddScoped<ISessionRowPlaceRepository, SessionRowPlaceRepository>();
builder.Services.AddScoped<IPricingRepository, PricingRepository>();

builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IOriginCountryService, OriginCountryService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IHallService, HallService>();
builder.Services.AddScoped<IRestrictionService, RestrictionService>();
builder.Services.AddScoped<IFilmGenreService, FilmGenreService>();
builder.Services.AddScoped<IFilmCountryService, FilmCountryService>();
builder.Services.AddScoped<ISessionRowPlaceService, SessionRowPlaceService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPricingService, PricingService>();



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
