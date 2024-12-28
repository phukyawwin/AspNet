using BlazorChatApp.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorChatApp.Client.Services
{
    public class MyHubConnectionService
    {

        private readonly HubConnection? _hubConnection;
        public bool IsConnected { get; set; }
        public MyHubConnectionService(NavigationManager navigationManager)
        {
            _hubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
            .Build();
            _hubConnection.StartAsync();
            GetConnectionState();

        }

        public bool GetConnectionState()
        {
            var hubConnection = GetConnection();
            IsConnected = hubConnection != null;
            return IsConnected;
        }

        public HubConnection GetConnection() => _hubConnection;
    }
}
