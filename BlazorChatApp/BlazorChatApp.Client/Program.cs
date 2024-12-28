using BlazorChatApp.Client.AppStates;
using BlazorChatApp.Client.Authentication;
using BlazorChatApp.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorChatApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<MyHubConnectionService>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
            builder.Services.AddScoped<AvailableUserState>();

            await builder.Build().RunAsync();
        }
    }
}
