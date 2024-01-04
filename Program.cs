using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BarberBookingWeb.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});



// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Programari");
    options.Conventions.AuthorizeFolder("/Clients");

    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/BarberShops");
    options.Conventions.AllowAnonymousToPage("/Books/Barbers");
    options.Conventions.AllowAnonymousToPage("/Books/Servicii");


    options.Conventions.AuthorizeFolder("/Clients", "AdminPolicy");
});
builder.Services.AddDbContext<BarberBookingWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BarberBookingWebContext") ?? throw new InvalidOperationException("Connection string 'BarberBookingWebContext' not found.")));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BarberBookingWebContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
