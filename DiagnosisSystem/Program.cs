global using Microsoft.EntityFrameworkCore;
using DiagnosisSystem.Controllers;
using DiagnosisSystem.Data;
using DiagnosisSystem.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(ConnectionString));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "DiagnosisSystem";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Adjust as needed
        options.LoginPath = "/Account/Login"; // Set your login path
        options.AccessDeniedPath = "/Account/AccessDenied"; // Set your access denied path
    });



// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<SignInManager<User>>();
//builder.Services.AddScoped<UserManager<User>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
