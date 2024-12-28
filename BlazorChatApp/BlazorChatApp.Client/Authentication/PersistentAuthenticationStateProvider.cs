using System.Security.Claims;
using BlazorChatApp.Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorChatApp.Client.Authentication
{
    public class PersistentAuthenticationStateProvider : AuthenticationStateProvider
    {
        private static readonly Task<AuthenticationState> defaultAuthenticationTask =
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        private readonly Task<AuthenticationState> authenticationTask = defaultAuthenticationTask;
        public PersistentAuthenticationStateProvider(PersistentComponentState state)
        {
            if (!state.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
                return;
            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier,userInfo.Id!),
                new Claim(ClaimTypes.Email,userInfo.Email!),
                new Claim(ClaimTypes.Name,userInfo.FullName!)
            ];
            authenticationTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, nameof(PersistentAuthenticationStateProvider)))))!;

        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        => authenticationTask;
    }
}
