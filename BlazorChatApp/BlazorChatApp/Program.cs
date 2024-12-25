using BlazorChatApp.Client.Services;
using BlazorChatApp.Components;
using BlazorChatApp.Hubs;
using BlazorChatApp.Infrastructure.Data;
using BlazorChatApp.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlazorChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            builder.Services.AddScoped<ChatRepository>();

            builder.Services.AddSignalR();
            builder.Services.AddScoped<ChatService>();
            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);
            app.MapControllers();
            app.MapHub<ChatHub>("/chathub");
            app.Run();
        }
    }
}
