using System;
using System.Text;
using AppointmentBookingSystem.Application.Common.Contract;
using AppointmentBookingSystem.Application.Common.Interfaces;
using AppointmentBookingSystem.Application.Common.Job;
using AppointmentBookingSystem.Application.Services.Implementation;
using AppointmentBookingSystem.Application.Services.Interface;
using AppointmentBookingSystem.Domain.Entities;
using AppointmentBookingSystem.Infrastructure.Data;
using AppointmentBookingSystem.Infrastructure.Email;
using AppointmentBookingSystem.Infrastructure.Job;
using AppointmentBookingSystem.Infrastructure.Repository;
using AppointmentBookingSystem.Web.Middlewares;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time   
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.LoginPath = "/Account/Index";
});
builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredLength = 6;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ISlotService, SlotService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddHangfire(options => options.UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire")));
builder.Services.AddHangfireServer();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<IJobService>("send-email-reminder-job", 
    x => x.StartSendReminderEmailAsync(),  
    "*/2 * * * *");

app.UseAuthentication();
app.UseAuthorization();
app.UseJwtMiddleware();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
