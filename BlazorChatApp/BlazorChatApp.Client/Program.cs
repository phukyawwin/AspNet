using BlazorChatApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorChatApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddScoped<ChatService>();
            await builder.Build().RunAsync();
        }
    }
}
