using Blog.Models;
using Blog.Data.Repository.iRepository;
using Blog.Data.Repository;
using Blog.Data;
using Microsoft.EntityFrameworkCore;
using BlogIdentity.Services.iServices;
using BlogIdentity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


/*builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("DefaultConnection"));*/

builder.Services.AddHttpClient<iUserService, UserService>();
builder.Services.AddHttpClient<iBlogService, BlogService>();

builder.Services.AddScoped<iUserService, UserService>();
builder.Services.AddScoped<iBlogService, BlogService>();

builder.Services.AddScoped<IEmailSender, EmailSender>();


builder.Services.AddRazorPages();

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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
