using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Repositories;
using ReservationSystem.Services;
using ReservationSystem.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Registrácia DbContext
builder.Services.AddDbContext<ReservationSystemDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registrácia Repositories
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationItemRepository, ReservationItemRepository>();
builder.Services.AddScoped<ISysConstRepository, SysConstRepository>();

//Registrácia Services
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationItemService, ReservationItemService>();
builder.Services.AddScoped<ISysConstService, SysConstService>();
builder.Services.AddScoped<AvailableDaysProvider>();

//Registrácia Util
builder.Services.AddScoped<SysConstUtil>();

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
